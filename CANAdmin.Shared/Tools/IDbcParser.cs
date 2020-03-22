using CANAdmin.Shared.Models;

namespace CANAdmin.Shared.Tools
{
    public interface IDbcParser
    {
        public CANDatabase ParseFile(FileModel file, ParsingArguments parsingArguments);
    }
}