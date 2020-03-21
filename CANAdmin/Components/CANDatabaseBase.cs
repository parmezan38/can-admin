using Microsoft.AspNetCore.Components;
using CANAdmin.Shared.Models;
using System.Threading.Tasks;

namespace CANAdmin.Components
{
    public class CANDatabaseBase : ComponentBase
    {
        [Parameter]
        public CANDatabaseView canDatabase { get; set; }
        public bool ShowMessages = false;
        public bool ShowNetworkNodes = false;
        [Parameter]
        public EventCallback<CANDatabaseView> DeleteWarningEventCallback { get; set; }

        public async Task DeleteWarning()
        {
            await DeleteWarningEventCallback.InvokeAsync(canDatabase);
        }
    }
}
