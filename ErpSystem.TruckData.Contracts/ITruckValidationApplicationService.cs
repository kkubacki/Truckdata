namespace ErpSystem.TruckData.Contracts
{
    public interface ITruckValidationApplicationService
    {
        Task<ValidationResultDto> Validate(TruckDto truckDto);
    }
}
