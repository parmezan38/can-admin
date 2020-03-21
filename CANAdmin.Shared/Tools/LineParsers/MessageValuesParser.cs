using System;

namespace CANAdmin.Shared.Tools.LineParsers
{
    class MessageValueParser
    {
        public static long MessageId(string line)
        {
            int start = line.IndexOf(' ') + 1;
            int end = line.IndexOf(' ', line.IndexOf(' ') + 1);
            string result = line.Substring(start, end - start);
            return long.Parse(result);
        }

        public static string Name(string line)
        {
            int start = line.IndexOf(' ', line.IndexOf(' ') + 1) + 1;
            int end = line.IndexOf(':');
            string result = line.Substring(start, end - start);
            return result;
        }
    }
}
