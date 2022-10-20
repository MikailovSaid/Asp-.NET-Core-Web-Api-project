using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApp.Core.Dtos
{
    public class ArticleCreateReqDto
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }
        public int ViewCount { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string? PhotoPath { get; set; }
        public int CategoryId { get; set; }
    }
}
