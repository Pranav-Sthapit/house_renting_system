using HouseRentalBackend.DTO;

namespace HouseRentalBackend.Services
{
    public interface IFlaskService
    {
        Task<ClusterResponseDTO> GetCluster(ClusterRequestDTO dto);
    }
}
