using Microsoft.AspNetCore.Components;
using CANAdmin.Components;
using CANAdmin.Services;
using CANAdmin.Shared.Models;
using System.Threading.Tasks;

namespace CANAdmin.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        private ICANDatabaseService CANDatabaseService { get; set; }
        public CANDatabaseList CANDatabaseListComponent;
        public WarningDialog WarningDialogComponent;
        public StatusMessage statusMessage;

        public void DeleteWarning(CANDatabaseView canDatabase)
        {
            WarningDialogComponent.ShowDialog(canDatabase);
        }

        public async Task SetStatusMessage(StatusMessage status)
        {
            statusMessage = status;
            await statusMessage.Run();
        }

        public async Task Delete(CANDatabaseView canDatabase)
        {
            var status = await CANDatabaseService.DeleteFile(canDatabase.Id);
            await RefreshCANDatabases();
            SetStatusMessage(status);
        }

        public async Task RefreshCANDatabases()
        {
            await CANDatabaseListComponent.RefreshCANDatabases();
        }
    }
}
