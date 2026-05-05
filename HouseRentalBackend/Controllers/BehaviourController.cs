using HouseRentalBackend.Repos.BehaviourRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BehaviourController : ControllerBase
    {
        private readonly IBehaviourRepository behaviourRepository;

        public BehaviourController(IBehaviourRepository behaviourRepository)
        {
            this.behaviourRepository = behaviourRepository;
        }

        [Authorize(Roles = "renter")]
        [HttpPut("view/{propertyId}")]
        public async Task<IActionResult> PropertyViewedCounter(int propertyId)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int renterId))
                return BadRequest("Invalid UserId format in token");

            await behaviourRepository.PropertyViewedCounter(renterId, propertyId);
            return Ok();
        }

        [HttpPut("apply/{propertyId}")]
        public async Task<IActionResult> AppliedForPropertyUpdate(int propertyId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("UserId not found in token");

            if (!int.TryParse(userId, out int renterId))
                return BadRequest("Invalid UserId format in token");

            await behaviourRepository.AppliedForPropertyUpdate(renterId, propertyId);
            return Ok();
        }
    }
}
