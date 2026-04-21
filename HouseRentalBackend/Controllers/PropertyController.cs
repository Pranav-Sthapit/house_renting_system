using HouseRentalBackend.DTO;
using HouseRentalBackend.Repos.PropertyRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository propertyRepository;
        public PropertyController(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }



        [Authorize(Roles = "renter")]
        [HttpGet("renter")]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await propertyRepository.GetAllProperties();
            return Ok(properties);
        }

        [Authorize(Roles = "owner")]
        [HttpGet("owner")]
        public async Task<IActionResult> GetOwnerProperties()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int ownerId))
                return BadRequest("Invalid UserId format in token");

            var properties = await propertyRepository.GetOwnerProperties(ownerId);
            return Ok(properties);
        }

        [Authorize]
        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetProperty(int propertyId)
        {
            var property = await propertyRepository.GetProperty(propertyId);
            return Ok(property);
        }


        [Authorize(Roles = "owner")]
        [HttpPost]
        public async Task<IActionResult> AddProperty([FromForm] PropertyRequestDTO dto)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int ownerId))
                return BadRequest("Invalid UserId format in token");

            var property = await propertyRepository.AddProperty(ownerId, dto);
            return Ok(property);
        }

        [Authorize(Roles = "owner")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromForm] PropertyUpdateDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int ownerId))
                return BadRequest("Invalid UserId format in token");


            var property = await propertyRepository.UpdateProperty(id, ownerId, dto);
            return Ok(property);

        }

        [Authorize(Roles = "owner")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int ownerId))
                return BadRequest("Invalid UserId format in token");



            var property = await propertyRepository.DeleteProperty(id, ownerId);
            return Ok(property);

        }
    }
}
