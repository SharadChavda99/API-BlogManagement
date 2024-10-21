using BlogMangementApi.Controllers;
using BlogMangementApi.Models;

namespace BlogMangementApi.Managers.BlogManager
{
    public interface IBlogManager
    {
        /// <summary>
        /// To get all the blog data
        /// </summary>
        /// <returns></returns>
        Task<List<BlogPost>> GetBlogsAsync();
        /// <summary>
        /// Add or update the blog details
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns></returns>
        Task AddOrUpdateBlogPostAsync(BlogPost blogPost);
        /// <summary>
        /// Deletes a blog by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteBlogPostAsync(int id);
        /// <summary>
        /// Get single blog detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BlogPost> GetBlogByIdAsync(int id);
        /// <summary>
        /// searches the text in blogtext and username
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task<List<BlogPost>> SearchBlogAsync(string text);
    }
}
