using ErpSystem.TruckData.Contracts;
using ErpSystem.TruckData.Domain.Entities;

namespace ErpSystem.TruckData.Application
{
    public class TruckApplicationService : ITruckApplicationService
    {
        private readonly ITruckRepository _truckRepository;
        private readonly ITruckValidationApplicationService _truckValidationApplicationService;
        private readonly IUnitOfWork _unitOfWork;

        public TruckApplicationService(ITruckRepository truckRepository,
            ITruckValidationApplicationService truckValidationApplicationService, IUnitOfWork unitOfWork)
        {
            _truckRepository = truckRepository;
            _truckValidationApplicationService = truckValidationApplicationService;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResultDto<TruckDto>> AddTruck(TruckDto truckDto)
        {
            var result = new OperationResultDto<TruckDto>
            {
                Data = truckDto
            };

            try
            {
                var truckDtoValidationResult = await _truckValidationApplicationService.Validate(truckDto);

                if (!truckDtoValidationResult.IsSuccess)
                {
                    result.Errors = truckDtoValidationResult.Errors.Select(x => $" {x.FieldName}, {x.Message}").ToList();
                    result.IsSuccess = false;

                    return result;
                }

                var truckEntity = TruckMapper.MapToTruck(truckDto);
                await _truckRepository.AddAsync(truckEntity);
                await _unitOfWork.CommitAsync();
                truckDto.SetId(truckEntity.Id);
                result.IsSuccess = true;

                return result;
            }

            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors.Add($"Failed To Add Truck ,ex message {ex.Message}");
                
                return result;
            }
        }

        public async Task<OperationResultDto<TruckDto>> GetTruckById(Guid truckId)
        {
            var result = new OperationResultDto<TruckDto>();
            try
            {
                var truckEntity = await _truckRepository.GetByIdAsync(truckId);
                var truckDto = TruckMapper.MapFromTruck(truckEntity);

                result.Data = truckDto;
                result.IsSuccess = true;

                return result;
            }

            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors = new List<string>() { $"Failed to Get Truck by Id ,ex message {ex.Message}" };

                return result;
            }
        }

        public async Task<OperationResultDto<TruckDto>> Delete(Guid truckId)
        {
            var result = new OperationResultDto<TruckDto>();
            try
            {
                var truckEntity = await _truckRepository.GetByIdAsync(truckId);

                if (truckEntity == null)
                {
                    result.IsSuccess = false;
                    result.Errors = new List<string>() { $"Truck with Id {truckId} not found." };

                    return result;
                }

                _truckRepository.Delete(truckEntity);
                await _unitOfWork.CommitAsync();

                result.IsSuccess = true;

                return result;
            }

            catch (Exception ex)
            {
                result.Errors.Add($"Failed to Delete Truck, ex message {ex.Message}");
                result.IsSuccess = false;
                return result;
            }
        }

        public async Task<OperationResultDto<TruckDto>> UpdateTruck(TruckDto truckDto)
        {
            var result = new OperationResultDto<TruckDto>
            {
                Data = truckDto
            };

            try
            {
                var truckDtoValidationResult = await _truckValidationApplicationService.Validate(truckDto);

                if (!truckDtoValidationResult.IsSuccess)
                {
                    result.Errors = truckDtoValidationResult.Errors.Select(x => $" {x.FieldName}, {x.Message}").ToList();
                    result.IsSuccess = false;

                    return result;
                }

                var truckEntityFromDto = TruckMapper.MapToTruck(truckDto);
                var truckEntityFromDb = await _truckRepository.GetByIdAsync(truckDto.Id);

                truckEntityFromDb.Update(truckEntityFromDto);
                _truckRepository.Update(truckEntityFromDb);
                await _unitOfWork.CommitAsync();

                truckDto.SetId(truckEntityFromDto.Id);
                result.IsSuccess = true;

                return result;
            }

            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors.Add($"Failed To Add Truck ,ex message {ex.Message}");

                return result;
            }
        }

        public async Task<OperationResultDto<IEnumerable<TruckDto>>> GetTrucksAsync(TruckQuery truckQuery)
        {
            var result = new OperationResultDto<IEnumerable<TruckDto>>();

            Func<IQueryable<Truck>, IQueryable<Truck>> filterFunc = null;
            Func<IQueryable<Truck>, IOrderedQueryable<Truck>> orderByFunc = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(truckQuery.Filter) && !string.IsNullOrWhiteSpace(truckQuery.FilterBy))
                {
                    if (string.Equals(truckQuery.FilterBy, "Name", StringComparison.OrdinalIgnoreCase))
                    {
                        filterFunc = q => q.Where(t => t.Name.Contains(truckQuery.Filter));
                    }
                    else if (string.Equals(truckQuery.FilterBy, "Code", StringComparison.OrdinalIgnoreCase))
                    {
                        filterFunc = q => q.Where(t => t.Code.Contains(truckQuery.Filter));
                    }
                }

                if (!string.IsNullOrWhiteSpace(truckQuery.OrderBy))
                {
                    if (string.Equals(truckQuery.OrderBy, "Name", StringComparison.OrdinalIgnoreCase))
                    {
                        orderByFunc = q => q.OrderBy(t => t.Name);
                    }
                    else if (string.Equals(truckQuery.OrderBy, "Code", StringComparison.OrdinalIgnoreCase))
                    {
                        orderByFunc = q => q.OrderBy(t => t.Code);
                    }
                }

                var truckEntities = await _truckRepository.GetAsync(filterFunc, orderByFunc);
                var truckDtos = truckEntities.Select(TruckMapper.MapFromTruck).ToList();
                result.Data = truckDtos;
                result.IsSuccess = true;

                return result;
            }

            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors.Add($"Failed to Get Truck by Query ,ex message {ex.Message}");

                return result;
            }
        }
    }
}
