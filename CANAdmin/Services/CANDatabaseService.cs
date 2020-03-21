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

        public async Task AddFile(IFileListEntry file)
        {
            var ms = new MemoryStream();
            await file.Data.CopyToAsync(ms);
            var content = new MultipartFormDataContent { { new ByteArrayContent(ms.GetBuffer()), "\"upload\"", file.Name } };

            var response = await _httpClient.PostAsync("api/can", content);
            return;
        }

        public async Task DeleteFile(int id)
        {
            await _httpClient.DeleteAsync($"api/can/{id}");
            return;
        }
    }
}
