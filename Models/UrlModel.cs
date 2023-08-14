using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskPr.Models
{
    public class UrlModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UrlId { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
