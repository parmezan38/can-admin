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

namespace CANAdmin.Api.Controllers
{
    [ApiController]
    [Route("api/can")]
    public class CANDatabaseController : Controller
    {
        private readonly ICANDatabaseManager _CANDatabaseManager;
        private string _fileLocation;

        public CANDatabaseController(ICANDatabaseManager CANDatabaseManager, IHostEnvironment env)
        {
            _CANDatabaseManager = CANDatabaseManager;
            _fileLocation = Path.Combine(env.ContentRootPath, "FileUploads", "dbcFile.dbc");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_CANDatabaseManager.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Add()
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
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0) return BadRequest();

                _CANDatabaseManager.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
