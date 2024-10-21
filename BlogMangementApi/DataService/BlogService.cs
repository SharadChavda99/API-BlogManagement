using BlogMangementApi.Models;
using System.Text.Json;

namespace BlogMangementApi.DataService
{
    public class BlogService : IBlogService
    {
        public List<BlogPost> _blogPosts;
        private static string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "BlogData.json");

        public BlogService()
        {
            _blogPosts = new List<BlogPost>();
        }

        public async Task LoadBlogPostsAsync()
        {
            try
            {
                if (File.Exists(jsonFilePath))
                {
                    string json = await File.ReadAllTextAsync(jsonFilePath);
                    _blogPosts = JsonSerializer.Deserialize<List<BlogPost>>(json);
                }
                else
                {
                    _blogPosts = new List<BlogPost>();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error loading blog posts.", ex);
            }
        }


        public async Task<List<BlogPost>> GetBlogPostsAsync()
        {
            await LoadBlogPostsAsync();
            return _blogPosts;
        }

        public async Task AddOrUpdateBlogPostAsync(BlogPost newPost)
        {
            var existingPost = _blogPosts.FirstOrDefault(blog => blog.Id == newPost.Id);

            if (existingPost != null)
            {
                existingPost.BlogText = newPost.BlogText;
                existingPost.Username = newPost.Username;
                existingPost.DateCreated = newPost.DateCreated;
            }
            else
            {
                if (_blogPosts.Any())
                {
                    int maxId = _blogPosts.Max(blog => blog.Id);
                    newPost.Id = maxId + 1;
                }
                else
                {
                    newPost.Id = 1;
                }
                _blogPosts.Add(newPost);
            }

            string updatedJson = JsonSerializer.Serialize(_blogPosts, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(jsonFilePath, updatedJson);
        }

        public async Task<bool> DeleteBlogPostAsync(int id)
        {
            var blogToDelete = _blogPosts.FirstOrDefault(blog => blog.Id == id);
            if (blogToDelete == null)
            {
                return false; //not found
            }

            _blogPosts.Remove(blogToDelete);

            string updatedJson = JsonSerializer.Serialize(_blogPosts, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(jsonFilePath, updatedJson);

            return true; // deleted
        }

        public async Task<BlogPost> GetBlogByIdAsync(int id)
        {
            var blogPost = _blogPosts.FirstOrDefault(b => b.Id == id);
            if (blogPost == null)
                throw new ApplicationException("No record found with Id : " + id);
            return await Task.FromResult(blogPost);
        }

        public async Task<List<BlogPost>> SearchBlogAsync(string text)
        {
            var result = _blogPosts
                .Where(blog => blog.BlogText.ToLower().Contains(text) ||
                               blog.Username.ToLower().Contains(text))
                .ToList(); 

            return await Task.FromResult(result);
        }


    }
}
