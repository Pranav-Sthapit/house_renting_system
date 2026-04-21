using HouseRentalBackend.DTO;
using HouseRentalBackend.Models;

namespace HouseRentalBackend.Repos
{
    public interface ILoginRepository
    {
        Task<Renter> LoginRenter(LoginDTO dto);
        Task<Owner> LoginOwner(LoginDTO dto);

        Task<Renter> RegisterRenter(RegisterDTO dto);

        Task<Owner> RegisterOwner(RegisterDTO dto);
    }
}
