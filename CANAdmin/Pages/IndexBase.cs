using CANAdmin.Components;
using CANAdmin.Services;
using CANAdmin.Shared.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System;

namespace CANAdmin.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        private ICANDatabaseService CANDatabaseService { get; set; }
        public CANDatabaseList CANDatabaseListComponent;
        public WarningDialog WarningDialogComponent;
        public StatusMessage statusMessage = new StatusMessage(null, null);
        private HubConnection connection;
        private int Time = 5;

        protected override async Task OnInitializedAsync()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44314/SignalHub")
                .WithAutomaticReconnect()
                .Build();

            await connection.StartAsync();

            connection.On("RefreshCANDatabaseList", async () => await CANDatabaseListComponent.RefreshCANDatabases());
            connection.On<string, string>("SetStatusMessage", (message, status) => SetStatusMessage(message, status));
        }

        public void DeleteWarning(CANDatabaseView canDatabase)
        {
            WarningDialogComponent.ShowDialog(canDatabase);
        }

        public async Task Delete(CANDatabaseView canDatabase)
        {
            await CANDatabaseService.DeleteFile(canDatabase.Id);
        }

        public async Task SetStatusMessage(string message, string status)
        {
            statusMessage = new StatusMessage(message, status);
            StateHasChanged();
            StartTimerAsync();
        }

        public async void StartTimerAsync()
        {
            Time = 5;
            while (Time > 0)
            {
                Time--;
                StateHasChanged();
                await Task.Delay(1000);
            }
            statusMessage = null;
            StateHasChanged();
        }
    }
}
