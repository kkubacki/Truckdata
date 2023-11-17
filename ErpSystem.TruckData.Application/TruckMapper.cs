using ErpSystem.TruckData.Contracts;
using ErpSystem.TruckData.Domain.Entities;
using ErpSystem.TruckData.Domain.Enums;

namespace ErpSystem.TruckData.Application
{
    internal class TruckMapper
    {
        public static Truck MapToTruck(TruckDto truckDto)
        {
            return new Truck(truckDto.Id, truckDto.Code, truckDto.Name, ParseTruckStatus(truckDto.Status), truckDto.Description);

        }

        public static TruckDto MapFromTruck(Truck truck)
        {
            if (truck == null)
            {
                throw new ArgumentNullException(nameof(truck));
            }

            return new TruckDto(truck.Id, truck.Code, truck.Name, truck.Status.GetDescription(), truck.Description);
        }

        private static TruckStatus ParseTruckStatus(string status)
        {

            if (Enum.TryParse<TruckStatus>(status, out var truckStatus))
            {
                return truckStatus;
            }

            throw new ArgumentException($"Invalid truck status: {status}");
        }
    }
}
