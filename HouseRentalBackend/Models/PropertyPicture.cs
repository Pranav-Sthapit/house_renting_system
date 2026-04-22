namespace HouseRentalBackend.Models
{
    public class PropertyPicture
    {  
        public int Id { get; set; }

        public required string FilePath { get; set; }

        public int PropertyId { get; set; }
        public Property? Property { get; set; }
    }
}
