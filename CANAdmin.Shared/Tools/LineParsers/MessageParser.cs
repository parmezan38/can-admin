using System.Collections.Generic;
using CANAdmin.Shared.Models;

namespace CANAdmin.Shared.Tools.LineParsers
{
    public class MessageParser
    {
        public static Message ParseLine(string line, string[] arguments)
        {
            Message message = new Message()
            {
                Signals = new List<Signal>()
            };

            foreach (string arg in arguments)
            {
                if (arg.Equals("MessageId"))
                {
                    message.MessageId = MessageValueParser.MessageId(line);
                }
                else if (arg.Equals("Name"))
                {
                    message.Name = MessageValueParser.Name(line);
                }
            }

            return message;
        }
    }
}
