using HouseRentalBackend.DTO;
using HouseRentalBackend.Repos.BehaviourRepo;
using HouseRentalBackend.Repos.RentalRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository rentalRepository;
        public RentalController(IRentalRepository rentalRepository)
        {
            this.rentalRepository = rentalRepository;
        }

        [Authorize(Roles = "renter")]
        [HttpGet("renter")]
        public async Task<IActionResult> GetRentalsOfRenter()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int renterId))
                return BadRequest("Invalid UserId format in token");

            var rentals = await rentalRepository.GetRentalsofRenter(renterId);
            return Ok(rentals);
        }

        [Authorize(Roles = "renter")]
        [HttpGet("renter/{propertyId}")]
        public async Task<IActionResult> GetRentalDetailsForRenter(int propertyId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int renterId))
                return BadRequest("Invalid UserId format in token");

            var rentalDetails = await rentalRepository.GetRentalDetailsForRenter(renterId, propertyId);
            return Ok(rentalDetails);
        }

        [Authorize(Roles = "owner")]
        [HttpGet("owner/{propertyId}")]
        public async Task<IActionResult> GetRentalsForOwner(int propertyId)
        {
            var rentals = await rentalRepository.GetRentalsForOwner(propertyId);
            return Ok(rentals);
        }

        [Authorize(Roles = "owner")]
        [HttpGet("owner/{propertyId}/{renterId}")]
        public async Task<IActionResult> GetRentalDetailsForOwner(int propertyId, int renterId)
        {

            var rentalDetails = await rentalRepository.GetRentalDetailsForOwner(propertyId, renterId);
            return Ok(rentalDetails);
        }

        [Authorize(Roles = "renter")]
        [HttpPost("renter/{propertyId}")]
        public async Task<IActionResult> AddRentalByRenter(int propertyId, [FromBody] RentalRequestAndUpdateDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int renterId))
                return BadRequest("Invalid UserId format in token");

            var rentalDetails = await rentalRepository.AddRentalByRenter(renterId, propertyId, dto);
            return Ok(rentalDetails);

        }

        [Authorize(Roles = "renter")]
        [HttpPut("renter/{propertyId}")]
        public async Task<IActionResult> UpdateRentalByRenter(int propertyId, [FromBody] RentalRequestAndUpdateDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int renterId))
                return BadRequest("Invalid UserId format in token");

            var rentalDetails = await rentalRepository.UpdateRentalByRenter(renterId, propertyId, dto);
            return Ok(rentalDetails);

        }

        [Authorize(Roles = "renter")]
        [HttpDelete("renter/{propertyId}")]
        public async Task<IActionResult> DeleteRentalByRenter(int propertyId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int renterId))
                return BadRequest("Invalid UserId format in token");

            var result = await rentalRepository.DeleteRentalByRenter(renterId, propertyId);
            return Ok(result);
        }

        [Authorize(Roles = "owner")]
        [HttpPut("owner/{propertyId}/{renterId}/approve")]
        public async Task<IActionResult> ApproveRentalByOwner(int propertyId, int renterId)
        {

            var rentalDetails = await rentalRepository.ApproveRentalByOwner(propertyId, renterId);
            return Ok(rentalDetails);
        }

        [Authorize(Roles = "owner")]
        [HttpPut("owner/{propertyId}/{renterId}/reject")]
        public async Task<IActionResult> RejectRentalByOwner(int propertyId, int renterId)
        {
            var rentalDetails = await rentalRepository.RejectRentalByOwner(propertyId, renterId);
            return Ok(rentalDetails);
        }
    }
}