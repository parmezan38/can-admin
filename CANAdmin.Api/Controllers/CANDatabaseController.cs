using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CANAdmin.Data;
using CANAdmin.Shared.Models;
using System.IO;
using System.Linq;
using System;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using CANAdmin.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using CANAdmin.Api.Services;

namespace CANAdmin.Api.Controllers
{
    [ApiController]
    [Route("api/can")]
    public class CANDatabaseController : Controller
    {
        private readonly ICANDatabaseManager _CANDatabaseManager;
        public readonly IHubContext<SignalHub> _signalHub;
        private readonly IEventMessageService _eventMessageService;
        private string _fileLocation;

        public CANDatabaseController(ICANDatabaseManager CANDatabaseManager, IHostEnvironment env, IHubContext<SignalHub> signalHub, IEventMessageService eventMessageService)
        {
            _CANDatabaseManager = CANDatabaseManager;
            _signalHub = signalHub;
            _eventMessageService = eventMessageService;
            _fileLocation = Path.Combine(env.ContentRootPath, "FileUploads", "dbcFile.dbc");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_CANDatabaseManager.Get());
        }

        [HttpPost]
        public async Task Add()
        {
            try
            {
                if (HttpContext.Request.Form.Files.Any())
                {
                    string fileName = "";
                    foreach (var file in HttpContext.Request.Form.Files)
                    {
                        using (var stream = new FileStream(_fileLocation, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        fileName = file.FileName;
                    }

                    FileModel savedFile = new FileModel(fileName, _fileLocation);
                    _CANDatabaseManager.Add(savedFile);

                    await _signalHub.Clients.All.SendAsync("RefreshCANDatabaseList");
                    _eventMessageService.Success("File uploaded successfully.");
                }
            }
            catch (Exception ex)
            {
                _eventMessageService.Error("Something went wrong while uploading the file");
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _eventMessageService.Error("Invalid CAN Database.");
                }
                _CANDatabaseManager.Delete(id);

                await _signalHub.Clients.All.SendAsync("RefreshCANDatabaseList");
                _eventMessageService.Success("CAN Database has been successfully deleted.");
            }
            catch (Exception ex)
            {
                _eventMessageService.Error("Something went wrong while trying to delete the CAN Database");
            }
        }
    }
}
