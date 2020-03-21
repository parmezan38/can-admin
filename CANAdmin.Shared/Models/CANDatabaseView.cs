using System;
using System.Collections.Generic;

namespace CANAdmin.Shared.Models
{
    public class CANDatabaseView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<MessageView> Messages { get; set; }
        public List<NetworkNodeView> NetworkNodes { get; set; }
    }
}
