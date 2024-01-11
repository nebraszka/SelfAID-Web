using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfAID.CommonLib.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; }

        [ForeignKey("ExerciseType")]
        public int ExerciseTypeId { get; set; }
        public virtual ExerciseType ExerciseType { get; set; }

        // Relacja 1 do wielu z Response
        public virtual ICollection<Response> Responses { get; set; }
    }
}