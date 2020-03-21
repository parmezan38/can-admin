namespace CANAdmin.Shared.Tools.LineParsers
{
    public class LinePrefix
    {
        public static bool CheckLine(string line, string checkString)
        {
            bool exists = false;
            char[] arr = checkString.ToCharArray();
            int length = arr.Length;

            if (line.Length < length) return false;

            for (int i = 0; i < length; i++)
            {
                exists = arr[i] == line[i];
                if (!exists) return exists;
            }
            return exists;
        }
    }
}
