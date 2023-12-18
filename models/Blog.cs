namespace efcrash;

using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

public class Blog
{
    public int BlogId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public List<Blog> GetAll() 
    {
        List<Blog> blogs = new List<Blog>();

        using (var dbContext = new BloggingContext()) 
        {
            try 
            {
                blogs = dbContext.Blogs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching blogs: {ex.Message}");
            }

            return blogs;
        }
    }

    public void Add(string name, string url) 
    {
        using (var dbContext = new BloggingContext()) 
        {
            try
            {
                var newBlog = new Blog
                {
                    Name = name,
                    Url = url
                };

                dbContext.Blogs.Add(newBlog);
                dbContext.SaveChanges();
                Console.WriteLine("Blog Added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occur while adding blogsite: {ex.Message}");
            }
        }
    }

}
