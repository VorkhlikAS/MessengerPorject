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
        public IActionResult Index()
        {
            //ViewBag.Contacts = _context.Contacts.Where(p => p.Forbiden == false).ToList();

            ViewBag.Contacts = _contacts.GetUserContacts();
            ViewBag.NotConfirmedContacts = _contacts.GetNonConfirmedContacts();

            return View(_userManager.Users.ToList());
        }
        //public IActionResult Index() => View(_userManager.Users.ToList());
        
        public async Task<ActionResult> AddToContacts(string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;
                Contact contact = new Contact { SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier), RecieverId = id };


                //bool ok = true;
                /*var all = _context.contacts;
                foreach (var item in all)
                {
                    if ((item.FirstId == userId && item.SecondId == Id) || (item.FirstId == Id && item.SecondId == userId))
                    {
                        ok = false;
                        break;
                    }
                }*/

                //var contacts = _context.Contact.ToList();
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
