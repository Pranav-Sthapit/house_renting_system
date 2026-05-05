namespace HouseRentalBackend.Models
{
    public class RenterBehaviourWithProperty
    {
        public int RenterId { get; set; }

        public int PropertyId { get; set; }

        public int TimesViewed { get; set; }

        public bool Applied { get; set; }

        public Renter? Renter { get; set; }

        public Property? Property { get; set; }
    }
}
