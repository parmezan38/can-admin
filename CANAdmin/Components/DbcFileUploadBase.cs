using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using CANAdmin.Services;
using System.Linq;
using System.Threading.Tasks;
using CANAdmin.Shared.Models;

namespace CANAdmin.Components
{
    public class DbcFileUploadBase : ComponentBase
    {
        [Inject]
        private ICANDatabaseService CANDatabaseService { get; set; }
        [Parameter]
        public EventCallback<StatusMessage> StatusMessageEventCallback { get; set; }
        public IFileListEntry DbcFile { get; set; }
        public string Label { get; set; }
        public bool DoubleBuffer = false;
        public bool CanUpload = true;


        public void GetFile(IFileListEntry[] files)
        {
            DbcFile = files.FirstOrDefault();
            Label = DbcFile.Name;
        }

        public async Task UploadFile()
        {
            if (DbcFile == null) return;
            if (CanUpload)
            {
                CanUpload = false;
                await CANDatabaseService.AddFile(DbcFile);
                CanUpload = true;

                DbcFile = null;
                Label = null;
                DoubleBuffer = !DoubleBuffer; // Fixes an issue when last uploaded file won't upload again
            }
        }
    }
}
