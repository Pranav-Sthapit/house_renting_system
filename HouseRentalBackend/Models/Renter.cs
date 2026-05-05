using System.Data;

namespace HouseRentalBackend.Models
{
    public class Renter:Person
    {        
        public RenterInfo? RenterInfo { get; set; }

        public List<Rental>? Rentals { get; set; }

        public List<RenterBehaviourWithProperty>? RenterBehaviours { get; set; }
    }
}
