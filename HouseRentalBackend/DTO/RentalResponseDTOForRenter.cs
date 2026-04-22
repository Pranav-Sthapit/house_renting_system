using HouseRentalBackend.Models;

namespace HouseRentalBackend.DTO
{
    public class RentalResponseDTOForRenter
    {
        public int PropertyId { get; set; }

        public int BHK { get; set; }

        public int Size { get; set; }

        public required string Floor { get; set; }

        public required string Locality { get; set; }

        public required string City { get; set; }

        public required string Thumbnail { get; set; }

        public required string Status { get; set; }
    }

    public class RentalResponseDTOForRenterWithDetails : RentalResponseDTOForRenter
    {
        public required string AreaType { get; set; }

        public required string FurnishingStatus { get; set; }

        public required string Tenant { get; set; }

        public required string ProposedTenant { get; set; }

        public int Rent { get; set; }  

        public int ProposedRent { get; set; }

        public int Bathroom { get; set; }

        public required string AggrementOfTerms { get; set; }

        public List<string>? Pictures { get; set; }
    }


    public class RentalRequestAndUpdateDTO
    {
        public required string Tenant { get; set; }
        public required int Rent { get; set; }
    }


}
