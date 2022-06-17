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
        private readonly IMessages _messages;


        public ChatController(UserManager<User> userManager, ApplicationContext context, IChatMember members, IMessages messages)
        {
            _userManager = userManager;
            _context = context;
            _members = members;
            _messages = messages;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var memberChats = _context.ChatMembers.Where(m => m.UserId == user.Id).ToList();
                List<ChatList> chats = new List<ChatList>();
                foreach(ChatMember chat in memberChats)
                {
                    var myChat = _context.ChatList.Where(p => p.IsAGroup && (p.Id == chat.ChatId)).FirstOrDefault();
                    if (myChat != null)
                        chats.Add(myChat);
                }
                ViewBag.Chats = chats;
                return View(memberChats);
            }
            else 
                return RedirectToAction("Error");
            
        }
        public async Task<ActionResult> Chat(string id)
        {
            var members = _members.GetMembers(id);
            var user = await _userManager.GetUserAsync(User);
            bool ok = false;
            string name = "";
            foreach (var member in members)
                if (member.UserId == user.Id.ToString())
                {
                    name = member.ChatName;
                    ok = true;
                }
                    
            if (ok)
            {
                var chat = _context.ChatList.Where(m => m.Id.ToString() == id).First();
                ViewBag.ChatId = id;
                ViewBag.ChatName = name;
                ViewBag.messages = _messages.GetChatMessages(id);
                if (chat.IsAGroup)
                    ViewBag.Key = chat.Key;
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
                if (_context.ChatMembers.Where(p => (p.ChatName == name)&&(p.UserId == user.Id)).Count() == 0)
                {
                    ChatList chatList = new ChatList { IsAGroup = false, Key="", Name="" };
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
        
        public IActionResult CreateChat() => View();
        [HttpPost]
        public async Task<ActionResult> CreateChat(string name)
        {
            var key = Guid.NewGuid().ToString();
            ChatList newChat = new ChatList { Name = name, IsAGroup = true, Key=key};
            _context.Add(newChat);
            _context.SaveChanges();
            var user = await _userManager.GetUserAsync(User);
            ChatMember chatMember = new ChatMember { ChatId = newChat.Id, ChatName=name, UserId = user.Id};
            _context.Add(chatMember);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Join(string key)
        {
            if (_context.ChatList.Where(p => p.Key == key).Count() > 0)
            {
                var user = await _userManager.GetUserAsync(User);
                var chat = _context.ChatList.Where(p => p.Key == key).FirstOrDefault();
                if (_context.ChatMembers.Where(p => (p.ChatId == chat.Id)&&(p.UserId == user.Id)).Count() ==0)
                {
                    ChatMember newMember = new ChatMember { ChatId = chat.Id, UserId = user.Id, ChatName = chat.Name };
                    _context.Add(newMember);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Такого чата не существует");
                return RedirectToAction("Index");
            }
        }
        public async Task<ActionResult> Leave(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var chatMember = _context.ChatMembers.Where(p => (p.ChatId.ToString() == id)&&(p.UserId==user.Id)).FirstOrDefault();
            if (chatMember != null)
            {
                _context.ChatMembers.Remove(chatMember);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
