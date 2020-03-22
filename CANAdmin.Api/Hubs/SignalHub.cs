using System.Threading.Tasks;
using CANAdmin.Api.Services;
using Microsoft.AspNetCore.SignalR;

namespace CANAdmin.Api.Hubs
{
    public class SignalHub : Hub
    {
        private readonly IEventMessageService _eventMessageService;

        public SignalHub(IEventMessageService eventMessageService)
        {
            _eventMessageService = eventMessageService;
        }

        public override async Task OnConnectedAsync()
        {
            _eventMessageService.connectionId = Context.ConnectionId;
            var cs = Context.ConnectionId;
        }

        public async Task RefreshCANDatabaseList()
        {
            await Clients.All.SendAsync("RefreshCANDatabaseList");
        }
    }
}
