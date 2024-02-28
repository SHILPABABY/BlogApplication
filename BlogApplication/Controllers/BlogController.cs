using BlogApplication.Models;
using BlogApplication.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BlogApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [Authorize]
        [HttpPost]
        [Route("AddBlog")]
        public void AddBlog(Blog blog)
        {
          _blogRepository.SaveBlog(blog);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllBlog")]
        public IActionResult GetAllBlog()
        {
            var products = _blogRepository.GetAllBlog();
            return Ok(products);
        }
        [Authorize]
        [HttpGet]
        [Route("GetMyBlog/{id}")]
        public IActionResult GetMyBlog(int id)
        {
            var products = _blogRepository.GetMyBlog(id);
            return Ok(products);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateMyBlog")]
        public async Task<IActionResult> UpdateMyBlog(Blog blog)
        {
            var products = await _blogRepository.UpdateBlog(blog);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);

            
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteBlog/{bid}")]
        public async Task<IActionResult> DeleteBlog(int bid)
        {
            bool status = await _blogRepository.DeleteBlog(bid);
            if (bid == 0)
            {
                return BadRequest();
            }
           
            return Ok(status);

        }
        [Authorize]
        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment(Comment comment)
        {
           var result=await _blogRepository.AddComment(comment);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("GetComments/{bid}")]
        public IActionResult GetComments(int bid)
        {
            var comments = _blogRepository.GetComments(bid);
            return Ok(comments);
        }
    }
}
