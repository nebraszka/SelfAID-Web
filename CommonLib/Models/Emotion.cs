using System.ComponentModel.DataAnnotations;

namespace SelfAID.CommonLib.Models
{
    public class Emotion
    {
        [Key]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = "Empty";
    }
}
