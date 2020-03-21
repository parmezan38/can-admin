using BlazorInputFile;
using CANAdmin.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CANAdmin.Services
{
    public interface ICANDatabaseService
    {
        public Task<IEnumerable<CANDatabaseView>> GetAll();
        public Task AddFile(IFileListEntry file);
        public Task DeleteFile(int id);
    }
}