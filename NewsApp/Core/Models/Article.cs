using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NewsApp.Core.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }
        public int ViewCount { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
