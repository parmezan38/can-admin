using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace CANAdmin.Shared.Tools
{
    public class FileSaver : IFileSaver
    {
        private string _fileLocation;

        public FileSaver(IHostEnvironment env)
        {
            string root = env.ContentRootPath;
            _fileLocation = Path.Combine(root, "FileUploads", "dbcFile.dbc");
        }

        public FileModel SaveFile(IFormFile file)
        {
            using (var fileStream = new FileStream(_fileLocation, FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

            string fileName = file.FileName.Substring(0, file.FileName.Length - 4);

            return new FileModel() { Name = fileName, Location = _fileLocation };
        }
    }
}
