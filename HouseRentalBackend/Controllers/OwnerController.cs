using HouseRentalBackend.DTO;
using HouseRentalBackend.Repos.OwnerRepo;
using HouseRentalBackend.Repos.RenterRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository ownerRepository;

        public OwnerController(IOwnerRepository ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }

        [Authorize(Roles = "owner")]
        [HttpGet]
        public async Task<IActionResult> GetOwnerInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int ownerId))
                return BadRequest("Invalid UserId format in token");

            try
            {
                var ownerInfo = await ownerRepository.GetOwnerInfo(ownerId);
                return Ok(ownerInfo);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "owner")]
        [HttpPut]
        public async Task<IActionResult> UpdateOwnerInfo([FromForm] OwnerUpdateDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");
            if (!int.TryParse(userId, out int ownerId))
                return BadRequest("Invalid UserId format in token");

            try
            {
                var updatedOwnerInfo = await ownerRepository.UpdateOwnerInfo(ownerId, dto);
                return Ok(updatedOwnerInfo);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
