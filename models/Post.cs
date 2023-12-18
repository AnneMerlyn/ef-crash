namespace efcrash;

using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public int BlogId { get; set; }
    public Blog? Blog { get; set; }

    public List<Post> GetAll() 
    {
        List<Post> posts = new List<Post>();
        using (var dbContext = new BloggingContext()) 
        {
            try
            {
                posts = dbContext.Posts.ToList(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching Posts: {ex.Message}");
                throw;
            }
        }

        return posts;
    }

    public List<Post> GetPostFor(int blogId) 
    {
        List<Post> posts = new List<Post>();

        using (var dbContext = new BloggingContext()) 
        {
            try
            {
                Console.WriteLine($"Blog ID: {blogId}");
                posts = dbContext.Posts
                        .Where(p => p.BlogId == blogId )
                        .Include(p => p.Blog)
                        .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching Posts for BlogId {blogId}: {ex.Message}");
            }
        }

        return posts;
    }

    public Post GetPost(int postId)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var post = dbContext.Posts.FirstOrDefault(p => p.PostId == postId);

                if (post != null) {
                    return post;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching Blog Post: {ex.Message}");
            }
        }

        return new Post();
    }

    public void AddPost (int blogId, string title, string content) 
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var newPost = new Post
                {
                    BlogId = blogId,
                    Title = title,
                    Content = content
                };

                dbContext.Posts.Add(newPost);
                dbContext.SaveChanges();
                Console.WriteLine("Post Added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while Adding a Post: {ex.Message}");
            }
        }
    }

    public void UpdatePost (int postId, string title, string content)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var post = dbContext.Posts.FirstOrDefault(p => p.PostId == postId);

                if (post == null)
                {
                    Console.WriteLine("No Matching Post Found!");
                } 
                else 
                {
                    post.Title = title;
                    post.Content = content;
                    dbContext.SaveChanges();
                    Console.WriteLine("Post Updated");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Updating a Post: {ex.Message}");
            }
        }
    }

    public void DeletePost (Post post) 
    {
        using ( var dbContext = new BloggingContext())
        {
            try
            {
                dbContext.Posts.Remove(post);
                dbContext.SaveChanges();
                Console.WriteLine("Post deleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in deleting Post: {ex.Message}");
            }
           
        }
    }
}
