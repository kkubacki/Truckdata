using System.Threading;
using System.Threading.Tasks;

namespace ErpSystem.TruckData.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
