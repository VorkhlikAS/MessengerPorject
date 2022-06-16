using MessengerNetSix.Models;
using MessengerNetSix.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MessengerNetSix.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly IChatMember _members;

        public ChatController(UserManager<User> userManager, ApplicationContext context, IChatMember members)
        {
            _userManager = userManager;
            _context = context;
            _members = members;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(_context.ChatMembers.Where(m => m.UserId == user.Id).ToList());
        }
        public async Task<ActionResult> Chat(string id)
        {
            var members = _members.GetMembers(id);
            var user = await _userManager.GetUserAsync(User);
            bool ok = false;
            foreach (var member in members)
                if (member.UserId == user.Id.ToString())
                    ok = true;
            if (ok)
            {
                ViewBag.ChatId = id;
                return View(members);
            }
            else return RedirectToAction("Error");
        }
        public async Task<IActionResult> CreateDialogue(string id, string name)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                string chatId = null;
                if (_context.ChatMembers.Where(p => (p.ChatName == name)&&(p.UserId == user.Id)) == null)
                {
                    ChatList chatList = new ChatList { IsAGroup = false };
                    _context.Add(chatList);
                    await _context.SaveChangesAsync();
                    ChatMember firstMember = new ChatMember { UserId = user.Id, ChatName = name, ChatId = chatList.Id };
                    ChatMember secondMember = new ChatMember { UserId = id, ChatName = user.UserName, ChatId = chatList.Id };
                    _context.Add(firstMember);
                    _context.Add(secondMember);
                    await _context.SaveChangesAsync();
                    chatId = chatList.Id.ToString();
                }
                else
                {
                    var temp = _context.ChatMembers.Where(p => (p.ChatName == name) && (p.UserId == user.Id)).First();
                    chatId = temp.ChatId.ToString();
                }
                return RedirectToAction("Chat", new { id = chatId });
            }
            return RedirectToAction("Error");
        }
        //WIP
        public async Task<ActionResult> CreateChat()
        {
            return View();
        }
    }
}
