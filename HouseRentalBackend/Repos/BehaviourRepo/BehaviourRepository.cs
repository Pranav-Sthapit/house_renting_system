using HouseRentalBackend.Data;
using HouseRentalBackend.Exceptions;
using HouseRentalBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentalBackend.Repos.BehaviourRepo
{
    public class BehaviourRepository:IBehaviourRepository
    {
        private readonly ApplicationDbContext context;
        public BehaviourRepository(ApplicationDbContext context) { this.context = context; }

        public async Task PropertyViewedCounter(int renterId, int propertyId)
        {
            var record = await context.RenterBehaviourWithProperties.FirstOrDefaultAsync(rbp => rbp.RenterId == renterId && rbp.PropertyId == propertyId);
            if (record != null)
            {
                record.TimesViewed++;
                await context.SaveChangesAsync();
            }
            else
            {
                var property = await context.Properties.FindAsync(propertyId);
                if (property == null)
                {
                    throw new NotFoundException("Property doesnot exist");
                }
                var recordToAdd = new RenterBehaviourWithProperty
                {
                    RenterId = renterId,
                    PropertyId = propertyId,
                    TimesViewed = 1,
                    Applied = false
                };
                context.RenterBehaviourWithProperties.Add(recordToAdd);
                await context.SaveChangesAsync();
            }

        }

        public async Task AppliedForPropertyUpdate(int renterId, int propertyId)
        {
            var record = await context.RenterBehaviourWithProperties.FirstOrDefaultAsync(rbp => rbp.RenterId == renterId && rbp.PropertyId == propertyId);
            if (record != null)
            {
                record.Applied = true;
                await context.SaveChangesAsync();
            }
            else
            {
                var property = await context.Properties.FindAsync(propertyId);
                if (property == null)
                {
                    throw new NotFoundException("Property doesnot exist");
                }
                var recordToAdd = new RenterBehaviourWithProperty
                {
                    RenterId = renterId,
                    PropertyId = propertyId,
                    TimesViewed = 0,
                    Applied = true
                };
                context.RenterBehaviourWithProperties.Add(recordToAdd);
                await context.SaveChangesAsync();
            }
        }
    }
}
