using Dapper;
using HouseRentalBackend.Data;
using HouseRentalBackend.DTO;
using HouseRentalBackend.Exceptions;
using HouseRentalBackend.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HouseRentalBackend.Repos.RentalRepo
{

    public class RentalRepository : IRentalRepository
    {
        private readonly DapperContext context;

        public RentalRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task<List<RentalResponseDTOForRenter>> GetRentalsofRenter(int renterId)
        {
            var query = "Select P.\"Id\" as \"PropertyId\",P.\"BHK\",P.\"Size\",P.\"Floor\",P.\"Locality\",P.\"City\",P.\"Thumbnail\",R.\"Status\" from \"Rentals\" as R JOIN \"Properties\" P On R.\"PropertyId\"=P.\"Id\" Where \"RenterId\"=@RenterId;";

            using var connection = context.CreateConnection();
            var rentalDetails = await connection.QueryAsync<RentalResponseDTOForRenter>(query, new { RenterId = renterId });

            List<RentalResponseDTOForRenter> list = new List<RentalResponseDTOForRenter>();

            return rentalDetails.ToList();
        }

        public async Task<RentalResponseDTOForRenterWithDetails> GetRentalDetailsForRenter(int renterId, int propertyId)
        {
            var query = "Select P.\"Id\" as \"PropertyId\" ,P.\"BHK\",P.\"Size\",P.\"Floor\",P.\"AreaType\",P.\"Locality\",P.\"City\",P.\"FurnishingStatus\",P.\"Tenant\",R.\"Tenant\" as \"ProposedTenant\",P.\"Bathroom\",P.\"Rent\",R.\"Rent\" as \"ProposedRent\",P.\"AggrementOfTerms\",P.\"Thumbnail\",P.\"Latitude\",P.\"Longitude\",R.\"Status\" from \"Rentals\" as R JOIN \"Properties\" P On R.\"PropertyId\"=P.\"Id\" Where R.\"RenterId\"=@RenterId and P.\"Id\"=@PropertyId;";
            using var connection = context.CreateConnection();
            var rentalDetails = await connection.QueryFirstOrDefaultAsync<RentalResponseDTOForRenterWithDetails>(query, new { RenterId = renterId, PropertyId = propertyId });
            if (rentalDetails == null)
            {
                throw new NotFoundException("Rental details not found");
            }

            var query2="SELECT \"FilePath\" as \"Pictures\" FROM \"PropertyPictureList\" WHERE \"PropertyId\"=@PropertyId";
            var pics = await connection.QueryAsync<string>(query2, new { PropertyId=propertyId});
            rentalDetails.Pictures = pics.ToList();
            return rentalDetails;
        }

        public async Task<List<RentalResponseDTOForOwner>> GetRentalsForOwner(int propertyId)
        {
            var query = "Select Rr.\"Id\" as \"RenterId\" ,Rr.\"FirstName\",Rr.\"LastName\",Rr.\"Contact\",Rl.\"Tenant\" as \"ProposedTenant\",Rl.\"Rent\" as \"ProposedRent\",Rl.\"Status\" from \"Rentals\" Rl join \"Person\" Rr on Rl.\"RenterId\"=Rr.\"Id\" Where Rl.\"PropertyId\"=@PropertyId;";
            using var connection = context.CreateConnection();
            var rentalDetails = await connection.QueryAsync<RentalResponseDTOForOwner>(query, new { PropertyId = propertyId });
            List<RentalResponseDTOForOwner> list = new List<RentalResponseDTOForOwner>();
            return rentalDetails.ToList();
        }

        public async Task<RentalResponseDTOForOwnerWithDetails> GetRentalDetailsForOwner(int propertyId, int renterId)
        {
            
            var query = "Select Rr.\"FirstName\",Rr.\"LastName\",Rr.\"Address\",Rr.\"Email\",Rr.\"Contact\",Rr.\"ProfilePhoto\",Ri.\"Passport\",Ri.\"Citizenship\",P.\"Rent\",Rl.\"Rent\" as \"ProposedRent\",P.\"Tenant\",Rl.\"Tenant\" as \"ProposedTenant\",Rl.\"Status\" From ((((\"Person\" Rr LEFT Join \"RenterInfos\" Ri On Rr.\"Id\"=Ri.\"RenterId\") Join \"Rentals\" Rl On Rl.\"RenterId\"=Rr.\"Id\") Join \"Properties\" P On P.\"Id\"=Rl.\"PropertyId\" ) )Where Rr.\"Id\"=@RenterId and P.\"Id\"=@PropertyId;";
            using var connection = context.CreateConnection();
            var rentalDetails = await connection.QueryFirstOrDefaultAsync<RentalResponseDTOForOwnerWithDetails>(query, new { RenterId = renterId, PropertyId = propertyId });

            if (rentalDetails == null)
            {
                throw new NotFoundException("Rental Details not found");
            }
            
            return rentalDetails;
        }

        public async Task<RentalResponseDTOForRenterWithDetails> AddRentalByRenter(int renterId, int propertyId, RentalRequestAndUpdateDTO dto)
        {
            var query = "Insert into \"Rentals\" (\"RenterId\",\"PropertyId\",\"Tenant\",\"Rent\",\"Status\") Values (@RenterId,@PropertyId,@Tenant,@Rent,'pending');";
            using var connection = context.CreateConnection();
            var parameters = new
            {
                RenterId = renterId,
                PropertyId = propertyId,
                Tenant = dto.Tenant,
                Rent = dto.Rent
            };
            int success = await connection.ExecuteAsync(query, parameters);

            if (success > 0)
                return await GetRentalDetailsForRenter(renterId, propertyId);
            else
                throw new AlreadyExistException("Record already exists please check your details");
        }

        public async Task<RentalResponseDTOForRenterWithDetails> UpdateRentalByRenter(int renterId, int propertyId, RentalRequestAndUpdateDTO dto)
        {
            var query = "Update \"Rentals\" SET \"Tenant\"=@Tenant, \"Rent\"=@Rent WHERE \"RenterId\"=@RenterId AND \"PropertyId\"=@PropertyId;";
            using var connection = context.CreateConnection();
            var parameters = new
            {
                RenterId = renterId,
                PropertyId = propertyId,
                Tenant = dto.Tenant,
                Rent = dto.Rent
            };
            int success = await connection.ExecuteAsync(query, parameters);

            if (success > 0)
                return await GetRentalDetailsForRenter(renterId, propertyId);
            else
                throw new Exception("Unable to update fields");
        }


        public async Task<bool> DeleteRentalByRenter(int renterId, int propertyId)
        {
            var query = "Delete From \"Rentals\" WHERE \"RenterId\"=@RenterId AND \"PropertyId\"=@PropertyId;";
            using var connection = context.CreateConnection();
            var parameters = new
            {
                RenterId = renterId,
                PropertyId = propertyId
            };

            int success = await connection.ExecuteAsync(query, parameters);

            if (success > 0)
                return true;
            else
                throw new Exception("Unable to delete record");
        }

        public async Task<RentalResponseDTOForOwnerWithDetails> ApproveRentalByOwner(int propertyId, int renterId)
        {
            using var connection = context.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();
            Console.WriteLine($"Received request for rental details with propertyId: {propertyId} and renterId: {renterId}");
            try
            {
                var query1 = "UPDATE \"Rentals\" SET \"Status\" = 'approved' WHERE \"RenterId\" = @RenterId AND \"PropertyId\" = @PropertyId;";

                var query2 = "UPDATE \"Rentals\" SET \"Status\" = 'rejected' WHERE \"RenterId\" != @RenterId AND \"PropertyId\" = @PropertyId;";

                var parameters = new
                {
                    RenterId = renterId,
                    PropertyId = propertyId
                };

                int success1 = await connection.ExecuteAsync(query1, parameters, transaction);
                int success2 = await connection.ExecuteAsync(query2, parameters, transaction);

                transaction.Commit();
                return await GetRentalDetailsForOwner(propertyId, renterId);
                
            }
            catch
            {
                transaction.Rollback();
                throw  new Exception("Unable to approve rental");
            }
        }


        public async Task<RentalResponseDTOForOwnerWithDetails> RejectRentalByOwner(int propertyId, int renterId)
        {
            var query = "UPDATE \"Rentals\" SET \"Status\" = 'rejected' WHERE \"RenterId\" = @RenterId AND \"PropertyId\" = @PropertyId;";
            using var connection = context.CreateConnection();

            int success = await connection.ExecuteAsync(query, new { RenterId = renterId, PropertyId = propertyId });

            if (success > 0)
                return await GetRentalDetailsForOwner(propertyId, renterId);
            else
                throw new Exception("Unable to reject rental");

        }
    }
}
