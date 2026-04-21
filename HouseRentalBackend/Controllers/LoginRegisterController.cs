using HouseRentalBackend.DTO;
using HouseRentalBackend.Exceptions;
using HouseRentalBackend.Repos;
using HouseRentalBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginRegisterController : ControllerBase
    {
        private readonly ILoginRepository repository;
        private readonly JwtService jwtService;

        public LoginRegisterController(ILoginRepository repository, JwtService jwtService)
        {
            this.repository = repository; this.jwtService = jwtService;
        }

        [HttpPost("login/{category}")]
        public async Task<IActionResult> LoginUsers(string category, [FromBody] LoginDTO dto)
        {

            if (category == "renter")
            {
                var user = await repository.LoginRenter(dto);
                var token = jwtService.GenerateToken(user.Id, user.Username, "renter");
                return Ok(new { Token = token });
            }
            else
            {
                var user = await repository.LoginOwner(dto);
                var token = jwtService.GenerateToken(user.Id, user.Username, "owner");
                return Ok(new { Token = token });
            }

        }

        [HttpPost("register/{category}")]
        public async Task<IActionResult> RegisterUsers(string category, [FromForm] RegisterDTO dto)
        {

            if (category == "renter")
            {

                var user = await repository.RegisterRenter(dto);
                var token = jwtService.GenerateToken(user.Id, user.Username, "renter");
                return Ok(new { Token = token });

            }
            else
            {

                var user = await repository.RegisterOwner(dto);
                var token = jwtService.GenerateToken(user.Id, user.Username, "owner");
                return Ok(new { Token = token });
            }

        }



    }
}
