using ErpSystem.TruckData.Contracts;
using ErpSystem.TruckData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.TruckData.Infrastructure
{
    public class TruckEfRepository : ITruckRepository
    {
        private TruckDataDbContext _context;

        public TruckEfRepository(TruckDataDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Truck>> GetAllSync()
        {
            return await _context.Trucks.ToListAsync();
        }


        public async Task<Truck> GetByIdAsync(Guid id)
        {
            var truck =  await _context.Trucks.SingleOrDefaultAsync(x => x.Id == id);

            return truck;
        }

        public async Task<IEnumerable<Truck>> GetAsync(Func<IQueryable<Truck>, IQueryable<Truck>> filter = null,
            Func<IQueryable<Truck>, IOrderedQueryable<Truck>> orderBy = null)
        {

            var query = _context.Trucks.AsQueryable();

            if (filter != null)
            {
                query = filter(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public void Delete(Truck truck)
        { 
            _context.Remove(truck);
        }

        public async Task AddAsync(Truck truck)
        {
            await _context.Trucks.AddAsync(truck);
        }

        public void Update(Truck truck)
        {
            _context.Trucks.Update(truck);
        }
    }
}
