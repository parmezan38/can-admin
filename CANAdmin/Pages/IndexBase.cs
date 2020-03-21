using BlazorInputFile;
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

        public void DeleteWarning(CANDatabaseView canDatabase)
        {
            WarningDialogComponent.ShowDialog(canDatabase);
        }

        public async Task Delete(CANDatabaseView canDatabase)
        {
            await CANDatabaseService.DeleteFile(canDatabase.Id);
            await RefreshCANDatabases();
        }

        public async Task RefreshCANDatabases()
        {
            await CANDatabaseListComponent.RefreshCANDatabases();
        }
    }
}
