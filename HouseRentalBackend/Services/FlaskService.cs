using HouseRentalBackend.DTO;

namespace HouseRentalBackend.Services
{
    public class FlaskService:IFlaskService
    {
        private readonly HttpClient httpClient;
        public FlaskService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ClusterResponseDTO> GetCluster(ClusterRequestDTO dto)
        {
            Console.WriteLine("dto received");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(dto));
            var response = await httpClient.PostAsJsonAsync("predict-cluster", dto);
            response.EnsureSuccessStatusCode();
            var clusterResponse = await response.Content.ReadFromJsonAsync<ClusterResponseDTO>();
            Console.WriteLine("clusterResponse received");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(clusterResponse));
            return clusterResponse!;
        }
    }
}
