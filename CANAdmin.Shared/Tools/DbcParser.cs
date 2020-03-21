using System.IO;
using System.Collections.Generic;
using CANAdmin.Shared.Models;
using CANAdmin.Shared.Tools.LineParsers;

namespace CANAdmin.Shared.Tools
{
    public class DbcParser : IDbcParser
    {
        public CANDatabase ParseFile(FileModel file)
        {
            string line;
            CANDatabase dbcObject = new CANDatabase();
            dbcObject.Name = file.Name;
            dbcObject.Messages = new List<Message>();

            using FileStream fileStream = File.OpenRead(file.Location);
            using var streamReader = new StreamReader(fileStream);

            while ((line = streamReader.ReadLine()) != null)
            {
                if (LinePrefix.CheckLine(line, "BU_"))
                {
                    dbcObject.NetworkNodes = NetworkNodeParser.ParseLine(line);
                }
                else if (LinePrefix.CheckLine(line, "BO_"))
                {
                    string[] arguments = new string[] { "MessageId", "Name" };
                    Message message = MessageParser.ParseLine(line, arguments);

                    dbcObject.Messages.Add(message);
                }
                else if (LinePrefix.CheckLine(line, " SG_"))
                {
                    string[] arguments = new string[] { "Name", "StartBit", "Length" };
                    Signal signal = SignalParser.ParseLine(line, arguments);

                    int indexOfLast = dbcObject.Messages.Count - 1;
                    dbcObject.Messages[indexOfLast].Signals.Add(signal);
                }
            }
            return dbcObject;
        }
    }
}
