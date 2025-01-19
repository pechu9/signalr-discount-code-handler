using Microsoft.AspNetCore.SignalR;
using static signalr_discount_code_handler.CodesHub;

namespace signalr_discount_code_handler
{
    public sealed class CodesHub : Hub<ICodesHub>
    {
        public interface ICodesHub
        {
            Task RecieveMessage(string message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.RecieveMessage($"New Member ({Context.ConnectionId}): Joined");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.RecieveMessage($"{Context.ConnectionId}: {message}");
        }
    }
}
