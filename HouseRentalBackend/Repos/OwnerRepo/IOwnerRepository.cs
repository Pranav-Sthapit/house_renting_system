using HouseRentalBackend.DTO;

namespace HouseRentalBackend.Repos.OwnerRepo
{
    public interface IOwnerRepository
    {
        Task<OwnerResponseDTO> GetOwnerInfo(int ownerId);

        Task<OwnerResponseDTO> UpdateOwnerInfo(int ownerId, OwnerUpdateDTO dto);
    }
}
