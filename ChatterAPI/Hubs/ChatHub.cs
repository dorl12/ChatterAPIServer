using Microsoft.AspNetCore.SignalR;
namespace ChatterAPI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            if(Clients == null) { return; }
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}