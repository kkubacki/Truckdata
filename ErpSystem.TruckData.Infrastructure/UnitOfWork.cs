using ErpSystem.TruckData.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.TruckData.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(
            DbContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
