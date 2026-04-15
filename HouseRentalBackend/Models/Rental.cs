namespace HouseRentalBackend.Models
{
    public class Rental
    {
        public int PropertyId { get; set; }

        public required Property Property { get; set; } 

        public int RenterId{ get; set; }

        public required Renter Renter { get; set; }

        public required int Rent {  get; set; }

        public required string Tenant { get; set; }

        public string Status { get; set; } = "pending";
    }
}
