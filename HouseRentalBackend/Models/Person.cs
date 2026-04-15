namespace HouseRentalBackend.Models
{
    public class Person
    {
        public int Id {  get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Address { get; set; }

        public required string Email { get; set; }

        public required long Contact {  get; set; }

        public string? ProfilePhoto { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

    }
}
