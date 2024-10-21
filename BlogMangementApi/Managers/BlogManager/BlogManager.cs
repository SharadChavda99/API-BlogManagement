using BlogMangementApi.DataService;
using BlogMangementApi.Models;

namespace BlogMangementApi.Managers.BlogManager
{
    public class BlogManager : IBlogManager
    {
        private readonly IBlogService _blogService;

        public BlogManager(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<List<BlogPost>> GetBlogsAsync()
        {
            return await _blogService.GetBlogPostsAsync();
        }

        public async Task AddOrUpdateBlogPostAsync(BlogPost blogPost)
        {
            await _blogService.AddOrUpdateBlogPostAsync(blogPost);
        }

        public async Task<bool> DeleteBlogPostAsync(int id)
        {
            return await _blogService.DeleteBlogPostAsync(id);
        }

        public async Task<BlogPost> GetBlogByIdAsync(int id)
        {
            return await _blogService.GetBlogByIdAsync(id);
        }

        public async Task<List<BlogPost>> SearchBlogAsync(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                throw new ApplicationException("Please enter valid search text.");
            return await _blogService.SearchBlogAsync(text.ToLower());
        }
    }
}
