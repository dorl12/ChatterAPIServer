using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
namespace ChatterAPI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}