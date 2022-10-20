using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Core.Dtos;
using NewsApp.Core.Models;
using NewsApp.Data;
using NewsApp.Utilities.File;

namespace NewsApp.Controllers
{
   // [Authorize]
    public class ArticleController : BaseApiController
    {
        private readonly AppDbContext _context;
        public ArticleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-article-by-id")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            Article? article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article.Title);
        }

        [HttpGet("get-all-articles")]
        public async Task<IActionResult> GetAllArticles()
        {
            List<Article>? articles = await _context.Articles.ToListAsync();
            if (articles == null)
            {
                return NotFound();
            }
            return Ok(articles.ToList());
        }

        [HttpPost("create-article")]
        public async Task<IActionResult> CrateArticle([FromForm] ArticleCreateReqDto article)
        {
            if (!article.Photo.CheckFileType("image/"))
            {
                return BadRequest();
            }

            if (!article.Photo.CheckFileSize(1800))
            {
                return BadRequest();
            }
            await _context.Articles.AddAsync(new()
            {
                PhotoPath = article.Photo.FileName,
                Title = article.Title,
                SubTitle = article.SubTitle,
                Desc = article.Desc,
                ViewCount = article.ViewCount,
                Status = article.Status,
                CategoryId = article.CategoryId,
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete-article")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            Article? article = await _context.Articles.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("update-article")]
        public async Task<IActionResult> UpdateArticle(int id, [FromForm] ArticleCreateReqDto article)
        {
            var articleDb = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
            
            if (articleDb is null)
            {
                return NotFound();

            }
            if (!article.Photo.CheckFileType("image/"))
            {
                return BadRequest();
            }

            if (!article.Photo.CheckFileSize(1800))
            {
                return BadRequest();
            }
            if (article.Photo != null)
            {
                System.IO.File.Delete(articleDb.PhotoPath);
                articleDb.PhotoPath = article.Photo.FileName;
            }
            articleDb.Title = article.Title;
            articleDb.SubTitle = article.SubTitle;
            articleDb.CategoryId = article.CategoryId;
            articleDb.Desc = article.Desc;
            articleDb.ViewCount = article.ViewCount;
            articleDb.Status = article.Status;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
