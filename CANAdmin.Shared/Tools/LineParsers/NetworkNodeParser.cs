using System.Collections.Generic;
using System.Linq;
using CANAdmin.Shared.Models;

namespace CANAdmin.Shared.Tools.LineParsers
{
    public class NetworkNodeParser
    {
        public static List<NetworkNode> ParseLine(string line)
        {
            List<string> names = line.Split(' ').ToList();
            names.RemoveAt(0);

            List<NetworkNode> networkNodes = new List<NetworkNode>();
            foreach (string name in names)
            {
                networkNodes.Add(new NetworkNode() { Name = name });
            }
            return networkNodes;
        }
    }
}
