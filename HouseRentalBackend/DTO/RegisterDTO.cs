namespace HouseRentalBackend.DTO
{
    public class RegisterDTO
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Address { get; set; }

        public required string Email { get; set; }

        public required string Contact { get; set; }

        public IFormFile? ProfilePhoto { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

}
