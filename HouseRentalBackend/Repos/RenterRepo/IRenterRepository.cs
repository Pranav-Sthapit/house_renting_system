using HouseRentalBackend.DTO;
using HouseRentalBackend.Models;

namespace HouseRentalBackend.Repos.RenterRepo
{
    public interface IRenterRepository
    {
        Task<RenterResponseDTO> GetRenterInfo(int renterId);

        Task<RenterResponseDTO> UpdateRenterInfo(int renterId, RenterUpdateDTO dto);

    }
}
