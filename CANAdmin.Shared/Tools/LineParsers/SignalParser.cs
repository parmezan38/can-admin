using CANAdmin.Shared.Models;
using System.Collections.Generic;

namespace CANAdmin.Shared.Tools.LineParsers
{
    public class SignalParser
    {
        public static Signal ParseLine(string line, List<string> arguments)
        {
            Signal signal = new Signal();

            foreach (string arg in arguments)
            {
                if (arg.Equals("Name"))
                {
                    signal.Name = SignalValueParser.Name(line);
                }
                else if (arg.Equals("StartBit"))
                {
                    signal.StartBit = SignalValueParser.StartBit(line);
                }
                else if (arg.Equals("Length"))
                {
                    signal.Length = SignalValueParser.Length(line);
                }
            }

            return signal;
        }
    }
}
