using System.IO;
using System.Collections.Generic;
using CANAdmin.Shared.Models;
using CANAdmin.Shared.Tools.LineParsers;

namespace CANAdmin.Shared.Tools
{
    public class DbcParser : IDbcParser
    {
        public CANDatabase ParseFile(FileModel file, ParsingArguments arguments)
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
                    Message message = MessageParser.ParseLine(line, arguments.Messages);

                    dbcObject.Messages.Add(message);
                }
                else if (LinePrefix.CheckLine(line, " SG_"))
                {
                    Signal signal = SignalParser.ParseLine(line, arguments.Signals);

                    int indexOfLast = dbcObject.Messages.Count - 1;
                    dbcObject.Messages[indexOfLast].Signals.Add(signal);
                }
            }
            return dbcObject;
        }
    }
}
