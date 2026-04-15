using HouseRentalBackend.DTO;
using HouseRentalBackend.Repos.RenterRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenterController : ControllerBase
    {
        private readonly IRenterRepository renterRepository;
        public RenterController(IRenterRepository renterRepository)
        {
            this.renterRepository = renterRepository;
        }

        [Authorize(Roles = "renter")]
        [HttpGet]
        public async Task<IActionResult> GetRenterInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int renterId))
                return BadRequest("Invalid UserId format in token");

            try
            {
                var renterInfo = await renterRepository.GetRenterInfo(renterId);
                return Ok(renterInfo);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "renter")]
        [HttpPut]
        public async Task<IActionResult> UpdateRenterInfo([FromForm] RenterUpdateDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");
            if (!int.TryParse(userId, out int renterId))
                return BadRequest("Invalid UserId format in token");

            try
            {
                var updatedRenterInfo = await renterRepository.UpdateRenterInfo(renterId, dto);
                return Ok(updatedRenterInfo);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
