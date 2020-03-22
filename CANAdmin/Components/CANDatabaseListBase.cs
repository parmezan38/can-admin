using Microsoft.AspNetCore.Components;
using CANAdmin.Services;
using CANAdmin.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CANAdmin.Components
{
    public class CANDatabaseListBase : ComponentBase
    {
        [Inject]
        private ICANDatabaseService CANDatabaseService { get; set; }
        public IEnumerable<CANDatabaseView> CANDatabases { get; set; }
        [Parameter]
        public EventCallback<CANDatabaseView> DeleteWarningEventCallback { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RefreshCANDatabases();
        }

        public async Task DeleteWarning(CANDatabaseView canDatabase)
        {
            await DeleteWarningEventCallback.InvokeAsync(canDatabase);
            await RefreshCANDatabases();
        }

        public async Task RefreshCANDatabases()
        {
            CANDatabases = await CANDatabaseService.GetAll();
            StateHasChanged();
        }
    }
}
