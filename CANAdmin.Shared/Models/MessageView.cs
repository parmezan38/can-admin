using System.Collections.Generic;

namespace CANAdmin.Shared.Models
{
    public class MessageView
    {
        public virtual long MessageId { get; set; }
        public virtual string Name { get; set; }
        public virtual List<SignalView> Signals { get; set; }
    }
}
