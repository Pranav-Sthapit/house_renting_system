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
        private readonly IFlaskService flaskService;

        public PropertyRepository(ApplicationDbContext context, IFileService fileService, IFlaskService flaskService)
        {
            this.context = context;
            this.fileService = fileService;
            this.flaskService = flaskService;
        }

        public async Task<List<PropertyResponseDTO>> GetAllProperties()
        {
            var properties = await context.Properties.Select(p => new PropertyResponseDTO
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
                Latitude = p.Latitude,
                Longitude= p.Longitude,
                OwnerId = p.OwnerId
            }).ToListAsync();

            return properties;
        }

        public async Task<List<PropertyResponseDTO>> GetFilteredProperties(ClusterRequestDTO dto)
        {
            var clusterRequest =new ClusterRequestDTO
            {
                BHK = dto.BHK,
                Rent = dto.Rent,
                Size = dto.Size,
                Floor = dto.Floor,
                Area_Type = dto.Area_Type,
                Furnishing_Status = dto.Furnishing_Status,
                Tenant_Preferred = dto.Tenant_Preferred
            };
            

            var clusterResponse = await flaskService.GetCluster(clusterRequest);

            var properties = await context.Properties.Where(p => p.Cluster == clusterResponse.Cluster).Select(p => new PropertyResponseDTO
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
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                OwnerId = p.OwnerId
            }).ToListAsync();

            if(properties.Count == 0)
            {
                throw new NotFoundException("No properties found matching the criteria.");
            }
            return properties;

        }

        public async Task<List<PropertyResponseDTO>> GetOwnerProperties(int ownerId)
        {
            var properties = await context.Properties.Where(p => p.OwnerId == ownerId).Select(p => new PropertyResponseDTO
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
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                OwnerId = p.OwnerId
            }).ToListAsync();

            return properties;
        }

        public async Task<PropertyResponseDTO> GetProperty(int propertyId)
        {
            var property = await context.Properties.Include(p => p.PropertyPictures).FirstOrDefaultAsync(p => p.Id == propertyId);
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
                Latitude = property.Latitude,
                Longitude = property.Longitude,
                OwnerId = property.OwnerId,
                Pictures = property.PropertyPictures?.Select(pp => pp.FilePath).ToList()
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
            Directory.CreateDirectory(uploadPath);
            string thumbnailName = await fileService.SaveFileAsync(thumbnailFile, uploadPath);
            string thumbnailPath = $"/uploads/owner/{ownerId}/{thumbnailName}";
            string aggrementName = await fileService.SaveFileAsync(aggrementFile, uploadPath);
            string aggrementPath = $"/uploads/owner/{ownerId}/{aggrementName}";

            var clusterRequest = new ClusterRequestDTO
            {
                BHK = dto.BHK,
                Rent = dto.Rent,
                Size = dto.Size,
                Floor = dto.Floor,
                Area_Type = dto.AreaType,
                Furnishing_Status = dto.FurnishingStatus,
                Tenant_Preferred = dto.Tenant
            };

            var clusterResponse = await flaskService.GetCluster(clusterRequest);

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
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Cluster = clusterResponse.Cluster,
                OwnerId = ownerId,
                Owner = owner
            };

            context.Properties.Add(property);
            await context.SaveChangesAsync();

            var pictures = dto.Pictures;
            if (pictures != null && pictures.Count > 0)
            {
                string picUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/property/{property.Id}");
                Directory.CreateDirectory(picUploadPath);
                if (property.PropertyPictures == null)
                {
                    property.PropertyPictures = new List<PropertyPicture>();
                }
                foreach (var picture in pictures)
                {
                    string pictureName = await fileService.SaveFileAsync(picture, picUploadPath);
                    string picturePath = $"/uploads/property/{property.Id}/{pictureName}";
                    property.PropertyPictures.Add(new PropertyPicture
                    {
                        FilePath = picturePath
                    });
                }
            }
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
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                OwnerId = property.OwnerId,
                Pictures = property.PropertyPictures?.Select(pp => pp.FilePath).ToList()
            };

            return response;
        }

        public async Task<PropertyResponseDTO> UpdateProperty(int id, int ownerId, PropertyUpdateDTO dto)
        {
            var property = await context.Properties.Include(property => property.PropertyPictures).SingleOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId);

            if (property == null)
            {
                throw new NotFoundException("Property not found.");
            }

            if (dto.Thumbnail != null)
            {
                fileService.DeleteFile(property.Thumbnail);
                var thumbnailName = await fileService.SaveFileAsync(dto.Thumbnail, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/owner/{ownerId}"));
                string thumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/owner/{ownerId}");
                Directory.CreateDirectory(thumb);
                property.Thumbnail = $"/uploads/owner/{ownerId}/{thumbnailName}";
            }
            if (dto.AggrementOfTerms != null)
            {
                fileService.DeleteFile(property.AggrementOfTerms);
                var aggrementName = await fileService.SaveFileAsync(dto.AggrementOfTerms, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/owner/{ownerId}"));
                string thumb = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/owner/{ownerId}");
                Directory.CreateDirectory(thumb);
                property.AggrementOfTerms = $"/uploads/owner/{ownerId}/{aggrementName}";
            }
            if (dto.Pictures != null && dto.Pictures.Count > 0)
            {
                if (property.PropertyPictures == null)
                {
                    property.PropertyPictures = new List<PropertyPicture>();
                }
                else
                {
                    var existingPictures = await context.PropertyPictureList.Where(pp => pp.PropertyId == id).ToListAsync();
                    foreach (var pic in existingPictures)
                    {
                        fileService.DeleteFile(pic.FilePath);
                    }
                    context.PropertyPictureList.RemoveRange(existingPictures);
                }

                string picUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/property/{property.Id}");
                Directory.CreateDirectory(picUploadPath);
                foreach (var picture in dto.Pictures)
                {
                    string pictureName = await fileService.SaveFileAsync(picture, picUploadPath);
                    string picturePath = $"/uploads/property/{property.Id}/{pictureName}";
                    property.PropertyPictures.Add(new PropertyPicture
                    {
                        FilePath = picturePath
                    });
                }
            }

            var clusterRequest = new ClusterRequestDTO
            {
                BHK = dto.BHK,
                Rent = dto.Rent,
                Size = dto.Size,
                Floor = dto.Floor,
                Area_Type = dto.AreaType,
                Furnishing_Status = dto.FurnishingStatus,
                Tenant_Preferred = dto.Tenant
            };

            var clusterResponse = await flaskService.GetCluster(clusterRequest);

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
            property.Latitude = dto.Latitude;
            property.Longitude = dto.Longitude;
            property.Cluster = clusterResponse.Cluster;

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
                Latitude = property.Latitude,
                Longitude = property.Longitude,
                OwnerId = property.OwnerId,
                Pictures = property.PropertyPictures?.Select(pp => pp.FilePath).ToList()
            };
        }

        public async Task<PropertyResponseDTO> DeleteProperty(int id, int ownerId)
        {
            var property = await context.Properties.SingleOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId);
            if (property == null)
            {
                throw new NotFoundException("Property not found.");
            }

            fileService.DeleteFile(property.Thumbnail);
            fileService.DeleteFile(property.AggrementOfTerms);

            if (property.PropertyPictures != null)
            {
                foreach (var p in property.PropertyPictures)
                {
                    fileService.DeleteFile(p.FilePath);
                }
            }
            if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/property/{property.Id}")))
                Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"uploads/property/{property.Id}"), true);

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
                Latitude = property.Latitude,
                Longitude = property.Longitude,
                OwnerId = property.OwnerId
            };

            return response;
        }
    }
}
