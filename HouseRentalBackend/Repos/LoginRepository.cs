using HouseRentalBackend.Data;
using HouseRentalBackend.DTO;
using HouseRentalBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentalBackend.Repos
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext context;

        public LoginRepository(ApplicationDbContext context) => this.context = context;

        public async Task<Renter?> LoginRenter(LoginDTO dto)
        {
              return await context.Renters.SingleOrDefaultAsync<Renter>(r => r.Username==dto.Username && r.Password==dto.Password );
        }

        public async Task<Owner?> LoginOwner(LoginDTO dto)
        {
            return await context.Owners.SingleOrDefaultAsync<Owner>(o => o.Username == dto.Username && o.Password == dto.Password);
        }

        public async Task<Renter> RegisterRenter(RegisterDTO dto)
        {

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


            var file=dto.ProfilePhoto;
            if (file != null)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/renters/{renter.Id}");
                if(!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using(var stream = new FileStream(filePath, FileMode.Create))
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
