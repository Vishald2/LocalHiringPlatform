using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace LocalHiringPlatform.Api.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId =
                Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Console.WriteLine($"####");
            Console.WriteLine($"Connected User : {userId}");

            Console.WriteLine($"Connection Id : {Context.ConnectionId}");
            Console.WriteLine($"####");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(
                Exception? exception)
        {
            Console.WriteLine($"Client Disconnected : {Context.ConnectionId}");

            await base.OnDisconnectedAsync(exception);
        }

        public async Task Ping()
        {
            Console.WriteLine("Ping received.");

            await Clients.Caller.SendAsync(
                "Pong",
                "Hello from Server");
        }
    }
}
