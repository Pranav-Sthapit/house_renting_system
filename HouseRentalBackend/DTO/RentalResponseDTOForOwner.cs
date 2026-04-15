namespace HouseRentalBackend.DTO
{
    public class RentalResponseDTOForOwner
    {
        public int RenterId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public int Contact {  get; set; }

        public required string Tenant { get; set; }

        public required string ProposedTenant { get; set; }

        public int Rent { get; set; }

        public int ProposedRent { get; set; }

        public required string Status { get; set; }
    }

    public class RentalResponseDTOForOwnerWithDetails : RentalResponseDTOForOwner
    {
        public required string Address { get; set; }
        public required string Email { get; set; }
        public  string? ProfilePhoto { get; set; }
        public  string? Passport { get; set; }
        public  string? Citizenship { get; set; }
    }
}
