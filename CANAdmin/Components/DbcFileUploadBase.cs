using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using CANAdmin.Services;
using System.Linq;
using System.Threading.Tasks;

namespace CANAdmin.Components
{
    public class DbcFileUploadBase : ComponentBase
    {
        [Inject]
        private ICANDatabaseService CANDatabaseService { get; set; }
        [Parameter]
        public EventCallback<bool> RefreshEventCallback { get; set; }
        public IFileListEntry DbcFile { get; set; }
        public string Label { get; set; }
        public bool DoubleBuffer = false;

        public void GetFile(IFileListEntry[] files)
        {
            DbcFile = files.FirstOrDefault();
            Label = DbcFile.Name;
        }

        public async Task UploadFile()
        {
            if (DbcFile == null) return;
            await CANDatabaseService.AddFile(DbcFile);
            DbcFile = null;
            Label = null;
            DoubleBuffer = !DoubleBuffer; // Fixes an issue when last uploaded file won't upload again
            await RefreshEventCallback.InvokeAsync(true);
        }
    }
}
