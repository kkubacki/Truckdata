namespace ErpSystem.TruckData.Contracts
{
    public interface ITruckApplicationService
    {
        Task<OperationResultDto<TruckDto>> AddTruck(TruckDto truckDto);

        Task<OperationResultDto<TruckDto>> GetTruckById(Guid truckId);

        Task<OperationResultDto<IEnumerable<TruckDto>>> GetTrucksAsync(TruckQuery truckQuery);

        Task<OperationResultDto<TruckDto>> Delete(Guid truckId);

        Task<OperationResultDto<TruckDto>> UpdateTruck(TruckDto truckDto);

    }
}
