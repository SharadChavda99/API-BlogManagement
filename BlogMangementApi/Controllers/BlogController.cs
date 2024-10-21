using BlogMangementApi.Managers.BlogManager;
using BlogMangementApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Transactions;

namespace BlogMangementApi.Controllers
{
    [ApiController]
    [Route("Blog")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogManager _blogManager;
        private readonly ILogger<BlogController> _log;
        MethodBase _methodBaseCurrent = MethodBase.GetCurrentMethod();
        public BlogController(IBlogManager blogManager, ILogger<BlogController> log)
        {
            _blogManager = blogManager;
            _log = log;
        }

        [HttpGet("GetAllBlogData")]
        public async Task<IActionResult> GetBlogs()
        {
            try
            {
                var blogs = await _blogManager.GetBlogsAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                _log.LogError("Error in " + _methodBaseCurrent.DeclaringType.Name + " " + _methodBaseCurrent.Name + " : " + ex.Message, ex);
                return BadRequest(new { Message = ex.Message, IsError = true });
            }
        }

        [HttpPost("AddEditBlog")]
        public async Task<IActionResult> AddEditBlog([FromBody] BlogPost newBlog)
        {
            try
            {
                await _blogManager.AddOrUpdateBlogPostAsync(newBlog);
                return Ok(new { Message = "Blog saved successfully."});
            }
            catch (Exception ex)
            {
                _log.LogError("Error in " + _methodBaseCurrent.DeclaringType.Name + " " + _methodBaseCurrent.Name + " : " + ex.Message, ex);
                return BadRequest(new { Message = ex.Message, IsError = true });
            }
        }

        [HttpGet("GetBlogById/{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            try
            {
                var transactionDetail = await _blogManager.GetBlogByIdAsync(id);
                if (transactionDetail != null)
                    return Ok(transactionDetail);
                return NotFound("No Record found with Id : " + id);
            }
            catch (Exception ex)
            {
                _log.LogError("Error in " + _methodBaseCurrent.DeclaringType.Name + " " + _methodBaseCurrent.Name + " : " + ex.Message, ex);
                return BadRequest(new { Message = ex.Message, IsError = true });
            }
        }

        [HttpGet("SearchBlog/{text}")]
        public async Task<IActionResult> SearchBlog(string text)
        {
            try
            {
                var blogs = await _blogManager.SearchBlogAsync(text);
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                _log.LogError("Error in " + _methodBaseCurrent.DeclaringType.Name + " " + _methodBaseCurrent.Name + " : " + ex.Message, ex);
                return BadRequest(new { Message = ex.Message, IsError = true });
            }
        }

        [HttpDelete("DeleteBlog/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                var deleteSuccess = await _blogManager.DeleteBlogPostAsync(id);
                if (!deleteSuccess)
                {
                    return NotFound(new { Message = $"Blog post with Id {id} not found." });
                }
                return Ok(new { Message = "Blog post deleted successfully." });
            }
            catch (Exception ex)
            {
                _log.LogError("Error in " + _methodBaseCurrent.DeclaringType.Name + " " + _methodBaseCurrent.Name + " : " + ex.Message, ex);
                return BadRequest(new { Message = ex.Message, IsError = true });
            }
        }

    }
}