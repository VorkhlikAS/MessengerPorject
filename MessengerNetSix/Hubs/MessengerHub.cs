using MessengerNetSix.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MessengerNetSix.Hubs
{
    public class MessengerHub : Hub
    {
        /*public async Task SendMessage(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }*/
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendMessageSefl(string Reciever, string message)
        {
            
            await Clients.User(Reciever).SendAsync("ReceiveMessage", Reciever, message);
            
        }
    }
}
