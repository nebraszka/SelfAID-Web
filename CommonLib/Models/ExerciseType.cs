using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfAID.CommonLib.Models
{
    public class ExerciseType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name {get; set;} = string.Empty;

        //TODO może warto tu walnąć ICollection
    }
}
