using Microsoft.AspNetCore.Components;
using CANAdmin.Shared.Models;

namespace CANAdmin.Components
{
    public class MessageBase : ComponentBase
    {
        [Parameter]
        public MessageView message { get; set; }
        public bool ShowSignals = false;
    }
}
