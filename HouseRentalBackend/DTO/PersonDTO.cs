namespace HouseRentalBackend.DTO
{
    public class PersonDTO
    {
        public int? Id { get; set; }
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Address { get; set; }

        public required string Email { get; set; }

        public required string Contact { get; set; }

        public required string Username { get; set; }

        public string? Password { get; set; }
    }

    public class RenterUpdateDTO:PersonDTO
    {
        public IFormFile? ProfilePhoto { get; set; }
        public IFormFile? Citizenship { get; set; }
        public IFormFile? Passport { get; set; }
    }

    public class RenterResponseDTO:PersonDTO
    {
        public string? ProfilePhoto { get; set; }
        public string? Citizenship { get; set; }
        public string? Passport { get; set; }
    }

    public class OwnerUpdateDTO:PersonDTO
    {
        public IFormFile? ProfilePhoto { get; set; }
    }

    public class OwnerResponseDTO : PersonDTO
    {
        public string? ProfilePhoto { get; set; }
    }


}
