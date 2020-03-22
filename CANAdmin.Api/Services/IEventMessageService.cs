using Microsoft.AspNetCore.SignalR;

namespace CANAdmin.Api.Services
{
    public interface IEventMessageService
    {
        public string connectionId { get; set; }
        public void Success(string message);
        public void Error(string message);
    }
}