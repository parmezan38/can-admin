using CANAdmin.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using BlazorInputFile;
using System.IO;

namespace CANAdmin.Services
{
    public class CANDatabaseService : ICANDatabaseService
    {
        private readonly HttpClient _httpClient;
        public CANDatabaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44314/");
        }

        public async Task<IEnumerable<CANDatabaseView>> GetAll()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<CANDatabaseView>>
                (await _httpClient.GetStreamAsync("api/can"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<StatusMessage> AddFile(IFileListEntry file)
        {
            var ms = new MemoryStream();
            await file.Data.CopyToAsync(ms);
            var content = new MultipartFormDataContent { { new ByteArrayContent(ms.GetBuffer()), "\"upload\"", file.Name } };

            var response = await _httpClient.PostAsync("api/can", content);

            StatusMessage statusMessage = new StatusMessage()
            {
                Message = "File uploaded successfully.",
                Status = "success"
            };

            if (!response.IsSuccessStatusCode)
            {
                statusMessage.Message = "Something went wrong while uploading the file";
                statusMessage.Status = "danger";
            }

            return statusMessage;
        }

        public async Task<StatusMessage> DeleteFile(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/can/{id}");

            StatusMessage statusMessage = new StatusMessage
            {
                Message = "CAN Database has been successfully deleted.",
                Status = "success"
            };

            if (!response.IsSuccessStatusCode)
            {
                statusMessage.Message = "Something went wrong while trying to delete the CAN Database";
                statusMessage.Status = "danger";
            }

            return statusMessage;
        }
    }
}
