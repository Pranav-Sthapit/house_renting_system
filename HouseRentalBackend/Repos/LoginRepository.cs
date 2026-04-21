using HouseRentalBackend.Data;
using HouseRentalBackend.DTO;
using HouseRentalBackend.Exceptions;
using HouseRentalBackend.Models;
using Microsoft.EntityFrameworkCore;
using HouseRentalBackend.Exceptions;

namespace HouseRentalBackend.Repos
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext context;

        public LoginRepository(ApplicationDbContext context) => this.context = context;


        private async Task FindDuplicates(string type, string field)
        {
            bool exists = false;

            switch (type)
            {
                case "Email":
                    exists = await context.Renters.AnyAsync(r => r.Email == field)
                          || await context.Owners.AnyAsync(o => o.Email == field);
                    break;

                case "Username":
                    exists = await context.Renters.AnyAsync(r => r.Username == field)
                          || await context.Owners.AnyAsync(o => o.Username == field);
                    break;

                case "Contact":
                    exists = await context.Renters.AnyAsync(r => r.Contact == field)
                          || await context.Owners.AnyAsync(o => o.Contact == field);
                    break;
            }

            if (exists)
            {
                throw new DuplicateException(type);
            }
        }


        public async Task<Renter> LoginRenter(LoginDTO dto)
        {
            var renter = await context.Renters.SingleOrDefaultAsync<Renter>(r => r.Username == dto.Username && r.Password == dto.Password);

            if (renter == null)
                throw new NotFoundException("Invalid username or password");

            return renter;
        }

        public async Task<Owner> LoginOwner(LoginDTO dto)
        {
            var owner = await context.Owners.SingleOrDefaultAsync<Owner>(o => o.Username == dto.Username && o.Password == dto.Password);
            
            if (owner == null)
                throw new NotFoundException("Invalid username or password");
            
            return owner;
        }

        public async Task<Renter> RegisterRenter(RegisterDTO dto)
        {
            // Check Duplicates
            await FindDuplicates("Email", dto.Email);
            await FindDuplicates("Username", dto.Username);
            await FindDuplicates("Contact", dto.Contact);


            Renter renter = new Renter
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Contact = dto.Contact,
                Username = dto.Username,
                Password = dto.Password
            };

            context.Renters.Add(renter);
            await context.SaveChangesAsync();


            var file = dto.ProfilePhoto;
            if (file != null)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/renters/{renter.Id}");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                renter.ProfilePhoto = $"/uploads/renters/{renter.Id}/{fileName}";
                context.Renters.Update(renter);
                await context.SaveChangesAsync();
            }

            return renter;

        }

        public async Task<Owner> RegisterOwner(RegisterDTO dto)
        {
            // Check Duplicates
            await FindDuplicates("Email", dto.Email);
            await FindDuplicates("Username", dto.Username);
            await FindDuplicates("Contact", dto.Contact);


            Owner owner = new Owner
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Contact = dto.Contact,
                Username = dto.Username,
                Password = dto.Password
            };

            context.Owners.Add(owner);
            await context.SaveChangesAsync();


            var file = dto.ProfilePhoto;
            if (file != null)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/owner/{owner.Id}");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                owner.ProfilePhoto = $"/uploads/owner/{owner.Id}/{fileName}";
                context.Owners.Update(owner);
                await context.SaveChangesAsync();
            }


            return owner;
        }

    }
}
