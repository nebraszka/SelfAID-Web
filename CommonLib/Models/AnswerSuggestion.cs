using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfAID.CommonLib.Models
{
    public class AnswerSuggestion
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string Suggestion { get; set; }

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
    }
}