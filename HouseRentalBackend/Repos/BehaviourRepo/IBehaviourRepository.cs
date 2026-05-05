using System.Runtime.CompilerServices;

namespace HouseRentalBackend.Repos.BehaviourRepo
{
    public interface IBehaviourRepository
    {
        Task PropertyViewedCounter(int renterId, int propertyId);

        Task AppliedForPropertyUpdate(int renterId, int propertyId);

    }
}
