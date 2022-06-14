using MessengerNetSix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MessengerNetSix.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public ChatController(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index(string id)
        {
            return View(_userManager.Users.ToList());
        }

        /*public Task<ActionResult> CreateDiologue(string id)
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
        }*/
    }
}
