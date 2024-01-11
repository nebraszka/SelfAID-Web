using System.ComponentModel.DataAnnotations;

namespace SelfAID.CommonLib.Models
{
    public class Entry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        // Relacja 1 do wielu z EntryPage
        public virtual ICollection<EntryPage> EntryPages { get; set; }
    }
}
