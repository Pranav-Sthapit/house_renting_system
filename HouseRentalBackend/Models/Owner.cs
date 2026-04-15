namespace HouseRentalBackend.Models
{
    public class Owner:Person
    {
        
        public List<Property>? Properties { get; set; }
    }
}
