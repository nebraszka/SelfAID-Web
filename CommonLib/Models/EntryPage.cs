using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfAID.CommonLib.Models
{
    public class EntryPage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [ForeignKey("Entry")]
        public int EntryId { get; set; }
        public virtual Entry Entry { get; set; }

        [ForeignKey("Emotion")]
        public int EmotionId { get; set; }
        public virtual Emotion Emotion { get; set; }

        // Relacja 1 do wielu z Response
        public virtual ICollection<Response> Responses { get; set; }
    }
}
