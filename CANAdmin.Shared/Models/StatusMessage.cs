using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace CANAdmin.Shared.Models
{
    public class StatusMessage
    {
        public string Message { get; set; }
        public string Status { get; set; }

        public async Task Run()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            Message = null;
            Status = null;
        }
    }
}
