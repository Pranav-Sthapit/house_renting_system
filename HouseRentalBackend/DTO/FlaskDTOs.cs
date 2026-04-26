namespace HouseRentalBackend.DTO
{
    public class FlaskDTOs
    {
    }

    public class ClusterRequestDTO
    {
        public int? BHK { get; set; }

        public int? Rent { get; set; }

        public int? Size { get; set; }

        public string? Floor { get; set; }

        public string? Area_Type { get; set; }

        public string? Furnishing_Status { get; set; }

        public string? Tenant_Preferred { get; set; }
    }

    public class ClusterResponseDTO
    {
       public int Cluster { get; set; }
    }
}
