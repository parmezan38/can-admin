using Microsoft.AspNetCore.Components;
using CANAdmin.Shared.Models;
using System.Threading.Tasks;

namespace CANAdmin.Components
{
    public class WarningDialogBase : ComponentBase
    {
        public CANDatabaseView CANDatabase;
        public bool IsDialogVisible = false;
        [Parameter]
        public EventCallback<CANDatabaseView> DeleteEventCallback { get; set; }

        public void ShowDialog(CANDatabaseView can)
        {
            CANDatabase = can;
            IsDialogVisible = true;
        }

        public void HideDialog()
        {
            IsDialogVisible = false;
            CANDatabase = null;
        }

        public async Task Delete()
        {
            await DeleteEventCallback.InvokeAsync(CANDatabase);
            HideDialog();
        }
    }
}
