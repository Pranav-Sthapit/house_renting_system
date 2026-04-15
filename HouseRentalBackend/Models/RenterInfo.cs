namespace HouseRentalBackend.Models
{
    public class RenterInfo
    {
        public int Id { get; set; }
        public string? Passport { get; set;  }
        public string? Citizenship { get; set; }
        public int RenterId { get; set; }
        public required Renter Renter {  get; set; }
    }
}
