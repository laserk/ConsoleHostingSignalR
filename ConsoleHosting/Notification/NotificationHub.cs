using Microsoft.AspNet.SignalR;

namespace ConsoleHosting
{
    public class NotificationHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.broadcastMessage(message);
        }
    }
}