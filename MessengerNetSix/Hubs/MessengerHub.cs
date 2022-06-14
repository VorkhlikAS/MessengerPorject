using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MessengerNetSix.Hubs
{
    public class MessengerHub : Hub
    {
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }
    }
}
