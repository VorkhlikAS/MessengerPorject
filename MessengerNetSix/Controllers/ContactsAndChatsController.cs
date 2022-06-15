using MessengerNetSix.Models;
using MessengerNetSix.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MessengerNetSix.Controllers
{
    public class ContactsAndChatsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        private readonly IContacts _contacts;

        public ContactsAndChatsController(ApplicationContext context, UserManager<User> userManager, IContacts contacts)
        {
            _context = context;
            _userManager = userManager;
            _contacts = contacts;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? searchId)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Contacts = _contacts.GetUserContacts(user.Id);
            ViewBag.NotConfirmedContacts = _contacts.GetNonConfirmedContacts(user.Id.ToString());
            if (searchId == null) { 
                ViewBag.SearchResult = null;
            }
            else
            {
                ViewBag.SearchResult = _userManager.Users.Where(c => (c.UserName.Contains(searchId)) && (c.Id != user.Id)).ToList();
            }
            return View(_userManager.Users.ToList());

        }
        [HttpGet]
        public async Task<ActionResult> GetUsers(string userId)
        {
            var user = await _userManager.GetUserAsync(User);
            return PartialView(_userManager.Users.Where(c => (c.Id.Contains(userId)) && (c.Id != user.Id)).ToList());
        }
        
        public async Task<ActionResult> AddToContacts(string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;
                Contact contact = new Contact { SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier), RecieverId = id };
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction("Index");
        }
        
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var contactItem = _context.Contacts.Where(p => (p.RecieverId == user.Id && p.SenderId == id) || (p.RecieverId == id && p.SenderId == user.Id)).Where(p => p.Forbiden == false).First();
            contactItem.Forbiden = true;
            _context.Update(contactItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Accept(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var contactItem = _context.Contacts.Where(p => (p.RecieverId == user.Id && p.SenderId == id) || (p.RecieverId == id && p.SenderId == user.Id)).Where(p => p.Forbiden == false).First();
            contactItem.IsConfirmed = true;
            _context.Update(contactItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
