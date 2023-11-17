using ErpSystem.TruckData.Domain.Entities;

namespace ErpSystem.TruckData.Contracts
{
    public interface ITruckRepository
    {
        Task<IEnumerable<Truck>> GetAllSync();

        Task<Truck> GetByIdAsync(Guid id);

        Task<IEnumerable<Truck>> GetAsync(Func<IQueryable<Truck>, IQueryable<Truck>> filter = null,
            Func<IQueryable<Truck>, IOrderedQueryable<Truck>> orderBy = null);

        void Delete(Truck truck);

        Task AddAsync(Truck truck);

        void Update(Truck truck);
    }
}