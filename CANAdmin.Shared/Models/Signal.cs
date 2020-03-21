using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CANAdmin.Shared.Models
{
    public class Signal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Message Message { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int StartBit{ get; set; }
        [Required]
        public int Length { get; set; }
    }
}
