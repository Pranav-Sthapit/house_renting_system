using HouseRentalBackend.DTO;
using HouseRentalBackend.Models;

namespace HouseRentalBackend.Repos.PropertyRepo
{
    public interface IPropertyRepository
    {
        Task<List<PropertyResponseDTO>> GetAllProperties();
        Task<List<PropertyResponseDTO>> GetOwnerProperties(int ownerId);

        Task<PropertyResponseDTO?> GetProperty(int propertyId);
        Task<PropertyResponseDTO> AddProperty(int ownerId,PropertyRequestDTO dto);

        Task<PropertyResponseDTO> UpdateProperty(int id,int ownerId,PropertyUpdateDTO dto);

        Task<PropertyResponseDTO> DeleteProperty(int id,int ownerId);
    }
}
