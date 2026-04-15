using HouseRentalBackend.Data;
using HouseRentalBackend.DTO;
using HouseRentalBackend.Models;
using HouseRentalBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace HouseRentalBackend.Repos.OwnerRepo
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IFileService fileService;
        public OwnerRepository(ApplicationDbContext context, IFileService fileService)
        {
            this.context = context;
            this.fileService = fileService;
        }

        public async Task<OwnerResponseDTO> GetOwnerInfo(int ownerId)
        {
            var owner = await context.Owners.FindAsync(ownerId);

            if (owner == null)
            {
                throw new Exception("Owner not found");
            }

            var ownerResponse = new OwnerResponseDTO
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email,
                Contact = owner.Contact,
                Address = owner.Address,
                Username = owner.Username,
                ProfilePhoto = owner.ProfilePhoto,
                Password = owner.Password
            };

            return ownerResponse;
        }

        public async Task<OwnerResponseDTO> UpdateOwnerInfo(int ownerId, OwnerUpdateDTO dto)
        {
            var owner = await context.Owners.FindAsync(ownerId);
            if (owner == null)
            {
                throw new Exception("Owner not found");
            }
            owner.FirstName = dto.FirstName;
            owner.LastName = dto.LastName;
            owner.Email = dto.Email;
            owner.Contact = dto.Contact;
            owner.Address = dto.Address;
            owner.Username = dto.Username;
            if (dto.Password != null) owner.Password = dto.Password;

            if (dto.ProfilePhoto != null)
            {
                if (owner.ProfilePhoto != null) fileService.DeleteFile(owner.ProfilePhoto);
                var profilePhotoName = await fileService.SaveFileAsync(dto.ProfilePhoto, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/owner/{ownerId}"));
                owner.ProfilePhoto = $"/uploads/owner/{ownerId}/{profilePhotoName}";
            }

            await context.SaveChangesAsync();
            return await GetOwnerInfo(ownerId);
        }
    }
}
