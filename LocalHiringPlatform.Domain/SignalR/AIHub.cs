using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.SignalR
{
    public class AIHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("####");

            Console.WriteLine($"Claim UserId      : {Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value}");

            Console.WriteLine($"UserIdentifier    : {Context.UserIdentifier}");

            Console.WriteLine($"Connection Id     : {Context.ConnectionId}");

            Console.WriteLine("####");

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
