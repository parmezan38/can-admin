using Microsoft.AspNetCore.Http;

namespace CANAdmin.Shared.Tools
{
    public interface IFileSaver
    {
        public FileModel SaveFile(IFormFile file);
    }
}