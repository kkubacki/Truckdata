using ErpSystem.TruckData.Domain.Entities;

namespace ErpSystem.TruckData.Contracts
{
    public interface ITruckRepository
    {        
        Truck GetById(Guid id);
        void Add(Truck truck);
        void Update(Truck truck);
        void Delete(Guid id);
        List<Truck> GetAll();
    }
}
