using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CANAdmin.Data;
using CANAdmin.Shared.Tools;
using System.IO;
using System.Linq;
using System;

namespace CANAdmin.Api.Controllers
{
    [ApiController]
    [Route("api/can")]
    public class CANDatabaseController : Controller
    {
        private readonly ICANDatabaseManager _CANDatabaseManager;
        private readonly IFileSaver _FileSaver;
        public CANDatabaseController(ICANDatabaseManager CANDatabaseManager, IFileSaver FileSaver)
        {
            _CANDatabaseManager = CANDatabaseManager;
            _FileSaver = FileSaver;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_CANDatabaseManager.Get());
        }

        [HttpPost]
        public IActionResult Add()
        {
            try
            {
                if (HttpContext.Request.Form.Files.Any())
                {
                    var file = HttpContext.Request.Form.Files[0];
                    FileModel savedFile = _FileSaver.SaveFile(file);
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
