using BlogApplication.Models;

namespace BlogApplication.Repositories.Interfaces
{
    public interface IBlogRepository // definition of the method
    {
        void SaveBlog(Blog blog);
        IEnumerable<Blog> GetAllBlog();
        Task<Blog> UpdateBlog(Blog blog);
        IEnumerable<Blog> GetMyBlog(int uid);
        Task<bool>DeleteBlog(int bid);
        Task<Comment> AddComment(Comment comment);
        IEnumerable<Comment> GetComments(int bid);
    }
}
