using BlogApplication.Models;
using BlogApplication.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;


namespace BlogApplication.Repositories.Implementation
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _blogContext;
        public BlogRepository(BlogContext blogContext)   //eg constructor injection 
        {
            _blogContext = blogContext;
            
        }
        public IEnumerable<Blog> GetAllBlog()
        {
            return _blogContext.Blogs;
        }

        public IEnumerable<Blog> GetMyBlog(int uid)
        {
            return _blogContext.Blogs.Where(i=>i.Uid==uid);
        }

        public void SaveBlog(Blog blog)
        {
            _blogContext.Blogs.Add(blog);
            _blogContext.SaveChanges();
            
        }
        public async Task<Blog> UpdateBlog(Blog blog)
        {
            var result = await _blogContext.Blogs
                .FirstOrDefaultAsync(e => e.Bid == blog.Bid);          

            if (result != null)
            {
                result.Uid = blog.Uid;
                result.Title = blog.Title;
                result.Description = blog.Description;              
                result.ModifiedDate = blog.ModifiedDate;
                await _blogContext.SaveChangesAsync(); 

                return result;
            }

           return result;
        }
        public async Task<bool> DeleteBlog(int bid)
        {
            bool status = false;
            if (bid != 0)
            {
                
                var result = await _blogContext.Blogs.FirstOrDefaultAsync(e => e.Bid == bid);

                if (result != null)
                {
                    _blogContext.Comments.Where(w => w.Bid == bid).ExecuteDelete();
                    _blogContext.Blogs.Remove(result);
                    await _blogContext.SaveChangesAsync();
                    status = true;

                }
            }
            return status;            
        }
       

        public async Task<Comment> AddComment(Comment comment)
        {
            _blogContext.Comments.Add(comment);

            await _blogContext.SaveChangesAsync();
            return comment;
        }
        public IEnumerable<Comment> GetComments(int bid)
        {
            return _blogContext.Comments.Include(i=>i.UidNavigation).Where(i => i.Bid == bid);
        }
    }
}
