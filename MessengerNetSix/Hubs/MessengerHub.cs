using MessengerNetSix.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MessengerNetSix.Hubs
{
    public class MessengerHub : Hub
    {
        public async Task RegisterUser(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName.ToString());
        }
        public async Task SendGroupMessage(string groupName, string Sender, string message)
        {
            await Clients.Group(groupName.ToString()).SendAsync("ReceiveMessage", Sender ,message);
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
