namespace HouseRentalBackend.DTO
{
    public class PropertyDTO
    {
        public required int BHK { get; set; }

        public required int Rent { get; set; }

        public required int Size { get; set; }

        public required string Floor { get; set; }

        public required string AreaType { get; set; }

        public required string Locality { get; set; }

        public required string City { get; set; }

        public required string FurnishingStatus { get; set; }

        public required string Tenant { get; set; }

        public required int Bathroom { get; set; }

        
    }

    public class PropertyRequestDTO:PropertyDTO
    {
        public required IFormFile Thumbnail { get; set; }
        public required IFormFile AggrementOfTerms { get; set; }

        public List<IFormFile>? Pictures { get; set; }
    }
    public class PropertyResponseDTO:PropertyDTO
    {
        public  int Id { get; set; }

        public required string Thumbnail { get; set; }
        public required string AggrementOfTerms { get; set; }
        public List<string>? Pictures { get; set; }
        public  int OwnerId { get; set; }
    }

    public class PropertyUpdateDTO:PropertyDTO
    {
        public IFormFile? Thumbnail { get; set; }
        public IFormFile? AggrementOfTerms { get; set; }
        public List<IFormFile>? Pictures { get; set; }
    }
}
