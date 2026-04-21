using HouseRentalBackend.Data;
using HouseRentalBackend.DTO;
using HouseRentalBackend.Exceptions;
using HouseRentalBackend.Models;
using HouseRentalBackend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HouseRentalBackend.Repos.PropertyRepo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IFileService fileService;

        public PropertyRepository(ApplicationDbContext context, IFileService fileService)
        {
            this.context = context;
            this.fileService = fileService;
        }

        public async Task<List<PropertyResponseDTO>> GetAllProperties()
        {
            var properties = await context.Properties.Select(p=>new PropertyResponseDTO
            {
                Id = p.Id,
                BHK = p.BHK,
                Rent = p.Rent,
                Size = p.Size,
                Floor = p.Floor,
                AreaType = p.AreaType,
                Locality = p.Locality,
                City = p.City,
                FurnishingStatus = p.FurnishingStatus,
                Tenant = p.Tenant,
                Bathroom = p.Bathroom,
                Thumbnail = p.Thumbnail,
                AggrementOfTerms = p.AggrementOfTerms,
                OwnerId = p.OwnerId
            }).ToListAsync();

            return properties;
        }

        public async Task<List<PropertyResponseDTO>> GetOwnerProperties(int ownerId)
        {
            var properties = await context.Properties.Where(p=>p.OwnerId==ownerId).Select(p=>new PropertyResponseDTO
            {
                Id = p.Id,
                BHK = p.BHK,
                Size = p.Size,
                Rent = p.Rent,
                Floor = p.Floor,
                AreaType = p.AreaType,
                Locality = p.Locality,
                City = p.City,
                FurnishingStatus = p.FurnishingStatus,
                Tenant = p.Tenant,
                Bathroom = p.Bathroom,
                Thumbnail = p.Thumbnail,
                AggrementOfTerms = p.AggrementOfTerms,
                OwnerId = p.OwnerId
            }).ToListAsync();

            return properties;
        }

        public async Task<PropertyResponseDTO> GetProperty(int propertyId)
        {
            var property = await context.Properties.FindAsync(propertyId);
            if (property == null)
                throw new NotFoundException("Property not found");

            return new PropertyResponseDTO
            {
                Id = property.Id,
                BHK = property.BHK,
                Size = property.Size,
                Rent = property.Rent,
                Floor = property.Floor,
                AreaType = property.AreaType,
                Locality = property.Locality,
                City = property.City,
                FurnishingStatus = property.FurnishingStatus,
                Tenant = property.Tenant,
                Bathroom = property.Bathroom,
                Thumbnail = property.Thumbnail,
                AggrementOfTerms = property.AggrementOfTerms,
                OwnerId = property.OwnerId
            };
        }
        public async Task<PropertyResponseDTO> AddProperty(int ownerId, PropertyRequestDTO dto)
        {

            var aggrementFile = dto.AggrementOfTerms;
            var thumbnailFile = dto.Thumbnail;
            if (aggrementFile == null || thumbnailFile == null)
            {
                throw new Exception("Aggrement of terms file and thumbnail is required.");
            }

            var owner = await context.Owners.FindAsync(ownerId);

            if (owner == null)
            {
                throw new NotFoundException("Owner not found.");
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/owner/{ownerId}");
            
            string thumbnailName = await fileService.SaveFileAsync(thumbnailFile, uploadPath);
            string thumbnailPath = $"/uploads/owner/{ownerId}/{thumbnailName}";
            string aggrementName = await fileService.SaveFileAsync(aggrementFile, uploadPath);
            string aggrementPath = $"/uploads/owner/{ownerId}/{aggrementName}";


            Property property = new Property
            {
                BHK = dto.BHK,
                Size = dto.Size,
                Floor = dto.Floor,
                Rent = dto.Rent,
                AreaType = dto.AreaType,
                Locality = dto.Locality,
                City = dto.City,
                FurnishingStatus = dto.FurnishingStatus,
                Tenant = dto.Tenant,
                Bathroom = dto.Bathroom,
                AggrementOfTerms = aggrementPath,
                Thumbnail = thumbnailPath,
                OwnerId = ownerId,
                Owner = owner
            };

            context.Properties.Add(property);
            await context.SaveChangesAsync();

            PropertyResponseDTO response = new PropertyResponseDTO
            {
                Id = property.Id,
                BHK = property.BHK,
                Size = property.Size,
                Floor = property.Floor,
                Rent = property.Rent,
                AreaType = property.AreaType,
                Locality = property.Locality,
                City = property.City,
                FurnishingStatus = property.FurnishingStatus,
                Tenant = property.Tenant,
                Bathroom = property.Bathroom,
                Thumbnail = property.Thumbnail,
                AggrementOfTerms = property.AggrementOfTerms,
                OwnerId = property.OwnerId
            };

            return response;
        }
        
        public async Task<PropertyResponseDTO> UpdateProperty(int id,int ownerId, PropertyUpdateDTO dto)
        {
            var property = await context.Properties.SingleOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId);

            if (property == null)
            {
                throw new NotFoundException("Property not found.");
            }

            if(dto.Thumbnail != null) 
            {
                fileService.DeleteFile(property.Thumbnail);
                var thumbnailName = await fileService.SaveFileAsync(dto.Thumbnail, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/owner/{ownerId}"));
                property.Thumbnail = $"/uploads/owner/{ownerId}/{thumbnailName}";
            }
            if(dto.AggrementOfTerms != null)
            {
                fileService.DeleteFile(property.AggrementOfTerms);
                var aggrementName = await fileService.SaveFileAsync(dto.AggrementOfTerms, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/owner/{ownerId}"));
                property.AggrementOfTerms = $"/uploads/owner/{ownerId}/{aggrementName}";
            }

            property.BHK = dto.BHK;
            property.Size = dto.Size;
            property.Rent = dto.Rent;
            property.Floor = dto.Floor;
            property.AreaType = dto.AreaType;
            property.Locality = dto.Locality;
            property.City = dto.City;
            property.FurnishingStatus = dto.FurnishingStatus;
            property.Tenant = dto.Tenant;
            property.Bathroom = dto.Bathroom;

            context.Properties.Update(property);
            await context.SaveChangesAsync();

            return new PropertyResponseDTO
            {
                Id = property.Id,
                BHK = property.BHK,
                Rent = property.Rent,
                Size = property.Size,
                Floor = property.Floor,
                AreaType = property.AreaType,
                Locality = property.Locality,
                City = property.City,
                FurnishingStatus = property.FurnishingStatus,
                Tenant = property.Tenant,
                Bathroom = property.Bathroom,
                Thumbnail = property.Thumbnail,
                AggrementOfTerms = property.AggrementOfTerms,
                OwnerId = property.OwnerId
            };
        }

        public async Task<PropertyResponseDTO> DeleteProperty(int id,int ownerId)
        {
            var property = await context.Properties.SingleOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId);
            if (property == null)
            {
                throw new NotFoundException("Property not found.");
            }

            fileService.DeleteFile(property.Thumbnail);
            fileService.DeleteFile(property.AggrementOfTerms);

            context.Properties.Remove(property);
            await context.SaveChangesAsync();

            PropertyResponseDTO response = new PropertyResponseDTO
            {
                Id = property.Id,
                BHK = property.BHK,
                Size = property.Size,
                Floor = property.Floor,
                AreaType = property.AreaType,
                Rent = property.Rent,
                Locality = property.Locality,
                City = property.City,
                FurnishingStatus = property.FurnishingStatus,
                Tenant = property.Tenant,
                Bathroom = property.Bathroom,
                Thumbnail = property.Thumbnail,
                AggrementOfTerms = property.AggrementOfTerms,
                OwnerId = property.OwnerId
            };

            return response;
        }
    }
}
