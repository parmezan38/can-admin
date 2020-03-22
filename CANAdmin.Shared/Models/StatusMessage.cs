namespace CANAdmin.Shared.Models
{
    public class StatusMessage
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public StatusMessage(string message, string status)
        {
            Message = message;
            Status = status;
        }
    }
}
