using CANAdmin.Api.Hubs;
using CANAdmin.Shared.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CANAdmin.Api.Services
{
    public class EventMessageService : IEventMessageService
    {
        public string connectionId { get; set; }
        private readonly IHubContext<SignalHub> _signalHub;

        public EventMessageService(IHubContext<SignalHub> signalHub)
        {
            _signalHub = signalHub;
        }

        public void Success(string message)
        {
            SendMessage(message, "success");
        }

        public void Error(string message)
        {
            SendMessage(message, "danger");
        }

        private void SendMessage(string message, string status)
        {
            _signalHub.Clients.Client(connectionId).SendAsync("SetStatusMessage", message, status);
        }
    }
}
