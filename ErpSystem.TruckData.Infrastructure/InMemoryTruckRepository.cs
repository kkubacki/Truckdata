using ErpSystem.TruckData.Contracts;
using ErpSystem.TruckData.Domain.Entities;

namespace ErpSystem.TruckData.Infrastructure
{
    public class InMemoryTruckRepository : ITruckRepository
    {
        private readonly Dictionary<Guid, Truck> _items;

        public InMemoryTruckRepository()
        {
            _items = new Dictionary<Guid, Truck>();
        }

        public IEnumerable<Truck> GetAll()
        {
            return _items.Values;
        }

        public Truck GetById(Guid id)
        {
            _items.TryGetValue(id, out var item);
            return item;
        }

        public void Add(Truck truck)
        {
            _items[truck.Id] = truck;
        }

        public void Update(Truck updatedTruck)
        {
            if (_items.ContainsKey(updatedTruck.Id))
            {
                _items[updatedTruck.Id] = updatedTruck;
            }
        }

        public void Delete(Guid id)
        {
            if (_items.ContainsKey(id))
            {
                _items.Remove(id);
            }
        }

        List<Truck> ITruckRepository.GetAll()
        {
            return _items.Values.ToList();
        }
    }
}
