using MessengerNetSix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MessengerNetSix.Hubs
{
    public class MessengerHub : Hub
    {
        private readonly ApplicationContext _context;

        public MessengerHub(ApplicationContext context)
        {
            _context = context;
        }
        public async Task RegisterUser(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName.ToString());
        }
        public async Task SendGroupMessage(Message message)
        {
            await Clients.Group(message.ChatId.ToString()).SendAsync("ReceiveMessage", message);
            _context.Add(message);
            _context.SaveChanges();
        }



        public async Task SendMessage(string user, string message)
        {
            //await Clients.User(user).SendAsync("ReceiveMessage", user, message);
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendMessageChat(string Sender, List<string> Reciever, string message)
        {
            // foreach(var name in Reciever)
            await Clients.User(Sender).SendAsync("ReceiveMessage", Sender, message);
            //await Clients.All.SendAsync("ReceiveMessage", Sender, message);

        }
    }
}
