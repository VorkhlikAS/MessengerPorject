using MessengerNetSix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            ViewBag.Chats = _context.ChatList.Where(ChatList => ChatList.Id.ToString() == id).ToList();
            return View(_userManager.Users.ToList());
        }
        public async Task<ActionResult> CreateDialogue(string id, string name)
        {
            if (ModelState.IsValid)
            {
                if (true)
                {
                    var user = await _userManager.GetUserAsync(User);
                    ChatList chatList = new ChatList { IsAGroup = false };
                    _context.Add(chatList);
                    await _context.SaveChangesAsync();
                    ChatMember firstMember = new ChatMember { UserId = user.Id, ChatName = name, ChatId = chatList.Id };
                    ChatMember secondMember = new ChatMember { UserId = id, ChatName = user.UserName, ChatId = chatList.Id };
                    _context.Add(firstMember);
                    _context.Add(secondMember);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                

            }
            return RedirectToAction("Index");
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
