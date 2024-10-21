using BlogMangementApi.Models;

namespace BlogMangementApi.DataService
{
    public interface IBlogService
    {
        Task<List<BlogPost>> GetBlogPostsAsync();
        Task AddOrUpdateBlogPostAsync(BlogPost newPost);
        Task<bool> DeleteBlogPostAsync(int id);
        Task<BlogPost> GetBlogByIdAsync(int id);
        Task<List<BlogPost>> SearchBlogAsync(string text);
    }
}
