using BlogApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BlogApplication.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("AllowOrigin")]
    [ApiController]
    public class Login : ControllerBase
    {
        private IConfiguration _config;
        private readonly BlogContext _context;
       
        public Login(BlogContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }
       
        private User AuthenticateUser(User user)
        {
            User _user = null;          
            if (user.Email !=null  && user.Password !=null)
            {
                if(_context.Users.Any(x => x.Email == user.Email && x.Password == user.Password))
                    {
                    _user = _context.Users.Single(b => b.Email == user.Email);
                }

            }
           

            return _user;

        }

        private User AuthenticateUser1(User user)
        {
            User _user = null;
            if (user.Email == "a@gmail.com" && user.Password == "test")
            {
                _user = new User { Email = "Shilpa" };
            }
            return _user;

        }
       
        private string GenerateToken(User user) // generating the token
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials); //creating the token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
      
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Loginn(User user)
        {
            IActionResult response = Unauthorized(); // 
            var _user = AuthenticateUser(user);
            if(_user!=null)
            {
                var token = GenerateToken(user);
                response=Ok(new {token= token,uid=_user.Id});
            }
            return response;
        }

    } 
}
