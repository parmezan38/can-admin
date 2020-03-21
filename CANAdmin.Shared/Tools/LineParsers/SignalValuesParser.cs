using System;

namespace CANAdmin.Shared.Tools.LineParsers
{
    class SignalValueParser
    {
        public static string Name(string line)
        {
            int start = line.IndexOf('_') + 2;
            string firstSplit = line.Substring(start, line.Length - 1 - start);
            int end = firstSplit.IndexOf(' ');
            return firstSplit.Substring(0, end);
        }

        public static int StartBit(string line)
        {
            int start = line.IndexOf(':') + 2;
            int end = line.IndexOf('|');
            string result = line.Substring(start, end - start);
            return Int32.Parse(result);
        }

        public static int Length(string line)
        {
            int start = line.IndexOf('|');
            int end = line.IndexOf('@');
            int length = end - start - 1;
            string result = line.Substring(start + 1, length);
            return Int32.Parse(result);
        }
    }
}
