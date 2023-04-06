using BumbleBEEBuy_Pay.Model.DB;
using BumbleBEEBuy_Pay.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BumbleBEEBuy_Pay.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {

        BumbleBeeDbContext dbContext = new BumbleBeeDbContext();
        DataSet dataset = new DataSet();

        private readonly IConfiguration _config;

        public AdminLoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AdminLogin adminlog)
        {
            LoginResponseDTO loginRespond = new LoginResponseDTO();
            // TODO: Encrypt password
            var user = dbContext.AdminLogins
                .Where(t =>
                    t.UserName == adminlog.UserName &&
                    t.Userpassword == adminlog.Userpassword 
                   
                 )
                 .FirstOrDefault();

            if (user == null)
            {
                return BadRequest("User name or Password Error");
            }
            else
            {
                loginRespond.Mesaage = "User Logged correct";
                return Ok(loginRespond);
            }        
        }
    }

}

