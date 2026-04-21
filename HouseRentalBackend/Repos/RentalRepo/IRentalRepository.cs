using HouseRentalBackend.DTO;

namespace HouseRentalBackend.Repos.RentalRepo
{
    public interface IRentalRepository
    {

        Task<List<RentalResponseDTOForRenter>> GetRentalsofRenter(int renterId);

        Task<RentalResponseDTOForRenterWithDetails> GetRentalDetailsForRenter(int renterId, int propertyId);
        
        Task<List<RentalResponseDTOForOwner>> GetRentalsForOwner(int propertyId);

        Task<RentalResponseDTOForOwnerWithDetails> GetRentalDetailsForOwner(int propertyId, int renterId);

        Task<RentalResponseDTOForRenterWithDetails> AddRentalByRenter(int renterId,int propertyId,RentalRequestAndUpdateDTO dto);

        Task<RentalResponseDTOForRenterWithDetails> UpdateRentalByRenter(int renterId, int propertyId, RentalRequestAndUpdateDTO dto);

        Task<bool> DeleteRentalByRenter(int renterId, int propertyId);

        Task<RentalResponseDTOForOwnerWithDetails> ApproveRentalByOwner(int propertyId,int renterId);

        Task<RentalResponseDTOForOwnerWithDetails> RejectRentalByOwner(int propertyId,int renterId);

    }
}
