using CANAdmin.Shared.Models;
using CANAdmin.Shared.Tools;
using System.Collections.Generic;

namespace CANAdmin.Data
{
    public interface ICANDatabaseManager
    {
        public void Add(FileModel file);
        public List<CANDatabaseView> Get();
        public void Delete(int id);

    }
}
