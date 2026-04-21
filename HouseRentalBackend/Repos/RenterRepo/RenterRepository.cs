using HouseRentalBackend.Data;
using HouseRentalBackend.DTO;
using HouseRentalBackend.Exceptions;
using HouseRentalBackend.Models;
using HouseRentalBackend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace HouseRentalBackend.Repos.RenterRepo
{
    public class RenterRepository : IRenterRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IFileService fileService;

        public RenterRepository(ApplicationDbContext context, IFileService fileService)
        {
            this.context = context;
            this.fileService = fileService;
        }


        private async Task FindDuplicates(string type, string field, int renterId)
        {
            bool exists = false;

            switch (type)
            {
                case "Email":
                    exists = await context.Renters.AnyAsync(r => r.Email == field && r.Id != renterId)
                          || await context.Owners.AnyAsync(o => o.Email == field);
                    break;

                case "Username":
                    exists = await context.Renters.AnyAsync(r => r.Username == field && r.Id != renterId)
                          || await context.Owners.AnyAsync(o => o.Username == field);
                    break;

                case "Contact":
                    exists = await context.Renters.AnyAsync(r => r.Contact == field && r.Id != renterId)
                          || await context.Owners.AnyAsync(o => o.Contact == field);
                    break;
            }

            if (exists)
            {
                throw new DuplicateException(type);
            }
        }

        public async Task<RenterResponseDTO> GetRenterInfo(int renterId)
        {
            var renter = await context.Renters.Include(r => r.RenterInfo).FirstOrDefaultAsync(r => r.Id == renterId);

            if (renter == null)
            {
                throw new NotFoundException("Renter not found");
            }

            var renterResponse = new RenterResponseDTO
            {
                Id = renter.Id,
                FirstName = renter.FirstName,
                LastName = renter.LastName,
                Email = renter.Email,
                Contact = renter.Contact,
                Address = renter.Address,
                Username = renter.Username,
                ProfilePhoto = renter.ProfilePhoto,
                Password = renter.Password
            };

            if (renter.RenterInfo != null)
            {
                renterResponse.Citizenship = renter.RenterInfo.Citizenship;
                renterResponse.Passport = renter.RenterInfo.Passport;
            }

            return renterResponse;
        }


        public async Task<RenterResponseDTO> UpdateRenterInfo(int renterId, RenterUpdateDTO dto)
        {

            await FindDuplicates("Email", dto.Email,renterId);
            await FindDuplicates("Username", dto.Username,renterId);
            await FindDuplicates("Contact", dto.Contact,renterId);

            var renter = await context.Renters.Include(r => r.RenterInfo).FirstOrDefaultAsync(r => r.Id == renterId);
            if (renter == null)
            {
                throw new NotFoundException("Renter not found");
            }
            renter.FirstName = dto.FirstName;
            renter.LastName = dto.LastName;
            renter.Email = dto.Email;
            renter.Contact = dto.Contact;
            renter.Address = dto.Address;
            renter.Username = dto.Username;
            if(dto.Password != null) renter.Password = dto.Password;

            if (dto.ProfilePhoto != null)
            {
                if (renter.ProfilePhoto != null) fileService.DeleteFile(renter.ProfilePhoto);
                var profilePhotoName = await fileService.SaveFileAsync(dto.ProfilePhoto, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/renters/{renterId}"));
                renter.ProfilePhoto = $"/uploads/renters/{renterId}/{profilePhotoName}";
            }
            if (dto.Citizenship != null)
            {
                if (renter.RenterInfo == null)
                {
                    var citizenshipName = await fileService.SaveFileAsync(dto.Citizenship, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/renters/{renterId}"));
                    renter.RenterInfo = new RenterInfo { Citizenship = $"/uploads/renters/{renterId}/{citizenshipName}", Renter = renter };
                }
                else
                {
                    if (renter.RenterInfo.Citizenship != null) fileService.DeleteFile(renter.RenterInfo.Citizenship);
                    var citizenshipName = await fileService.SaveFileAsync(dto.Citizenship, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/renters/{renterId}"));
                    renter.RenterInfo.Citizenship = $"/uploads/renters/{renterId}/{citizenshipName}";
                }

            }
            if (dto.Passport != null)
            {
                if (renter.RenterInfo == null)
                {
                    var passportName = await fileService.SaveFileAsync(dto.Passport, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/renters/{renterId}"));
                    renter.RenterInfo = new RenterInfo { Passport = $"/uploads/renters/{renterId}/{passportName}", Renter = renter };
                }
                else
                {
                    if (renter.RenterInfo.Passport != null) fileService.DeleteFile(renter.RenterInfo.Passport);
                    var passportName = await fileService.SaveFileAsync(dto.Passport, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/renters/{renterId}"));
                    renter.RenterInfo.Passport = $"/uploads/renters/{renterId}/{passportName}";
                }

            }
            
            await context.SaveChangesAsync();
            return await GetRenterInfo(renterId);

        }
    }
}
