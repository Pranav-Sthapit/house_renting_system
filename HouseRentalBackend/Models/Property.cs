namespace HouseRentalBackend.Models
{
    public class Property
    {
        public int Id { get; set; }

        public required int Rent { get; set; }

        public required int BHK { get; set; }

        public required int Size { get; set; }

        public required string Floor { get; set; }

        public required string AreaType { get; set; }

        public required string Locality { get; set; }

        public required string City { get; set; }

        public required string FurnishingStatus { get; set; }

        public required string Tenant { get; set; }

        public required int Bathroom { get; set; }

        public required string Thumbnail { get; set; }

        public required string AggrementOfTerms { get; set; }

        public required double Latitude { get; set; }

        public required double Longitude { get; set; }

        public required int Cluster { get; set; }

        public int OwnerId { get; set; }   

        public required Owner Owner{  get; set; }

        public List<Rental>? Rentals { get; set; }

        public List<PropertyPicture>? PropertyPictures { get; set; }

        public List<RenterBehaviourWithProperty>? RenterBehaviours { get; set; }
    }
}
