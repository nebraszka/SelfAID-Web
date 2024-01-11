using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfAID.CommonLib.Models
{
    public class Response
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        [ForeignKey("EntryPage")]
        public int EntryPageId { get; set; }
        public virtual EntryPage EntryPage { get; set; }

        public string SelectedAnswer { get; set; } // Może być null dla odpowiedzi tekstowych
        public string TextAnswer { get; set; } // Może być null w zależności od typu pytania
    }
}