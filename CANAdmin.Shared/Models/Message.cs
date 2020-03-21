using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CANAdmin.Shared.Models
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public long MessageId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Signal> Signals { get; set; }
        public CANDatabase CANDatabase { get; set; }

    }
}
