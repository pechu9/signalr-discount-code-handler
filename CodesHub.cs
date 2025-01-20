using Microsoft.AspNetCore.SignalR;
using static signalr_discount_code_handler.CodesHub;

namespace signalr_discount_code_handler
{
    public sealed class CodesHub : Hub<ICodesClient>
    {
        public interface ICodesClient
        {
            Task ReceiveMessage(string message);
        }

        private readonly CodeService _codeGenerator;

        public CodesHub(CodeService codeGenerator)
        {
            _codeGenerator = codeGenerator;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"New Member ({Context.ConnectionId}): Joined");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId}: {message}");
        }

        public async Task<bool> GenerateCodes(int count, int codeLength)
        {
            if (count < 1 || count > 2000 || (codeLength != 7 && codeLength != 8))
            {
                Console.WriteLine("Invalid input.");
                return false;
            }

            var tasks = new List<Task<bool>>();
            for (int i = 0; i < count; i++)
            {
                tasks.Add(_codeGenerator.GenerateUniqueCodeAsync(codeLength));
            }
            var results = await Task.WhenAll(tasks);

            return results.All(r => r);
        }

        public async Task<int> UseCode(string code)
        {
            if (code.Length < 7 || code.Length > 8)
            {
                Console.WriteLine("Invalid input.");
                return StatusCodes.Status400BadRequest;
            }
            
            return await _codeGenerator.UseCode(code);
        }
    }
}
