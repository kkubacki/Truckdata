using ErpSystem.TruckData.Contracts;
using ErpSystem.TruckData.Domain.Entities;
using ErpSystem.TruckData.Domain.Services;

namespace ErpSystem.TruckData.Application
{
    public class TruckValidationApplicationService : ITruckValidationApplicationService
    {
        private readonly ITruckRepository _truckRepository;

        public TruckValidationApplicationService(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public async Task<ValidationResultDto> Validate(TruckDto truckDto)
        {
            var errorList = new List<ValidationErrorDto>();


            if (!TruckValidationService.ValidateStatusString(truckDto.Status)) {
                
                errorList.Add(new ValidationErrorDto("Status", $"{TruckValidationService.StatusValidationError}, '{truckDto.Status}' is not Valid"));
            }


            if (!TruckValidationService.ValidateName(truckDto.Name))
            {
                errorList.Add(new ValidationErrorDto("Name", $"{TruckValidationService.NameErrorMessage}, '{truckDto.Name}' is not Valid"));
            }

            if (!TruckValidationService.ValidateCode(truckDto.Code))
            {
                errorList.Add(new ValidationErrorDto("Code", $"{TruckValidationService.CodeErrorMessage}, '{truckDto.Code}' is not Valid"));
            }

            bool doTruckWihRequestedCodeExistsInDb = await CheckIfExistsTruckWithCode(truckDto.Code);

            if (doTruckWihRequestedCodeExistsInDb)
            {
                errorList.Add(new ValidationErrorDto("Code",$"truck with '{truckDto.Code}' code exists already in database."));
            }

            if (errorList.Any())
            {
                return ValidationResultDto.CreateFail(errorList);
            }

            return ValidationResultDto.CreateSuccess();
        }

        private async Task<bool> CheckIfExistsTruckWithCode(string truckCode)
        {
            Func<IQueryable<Truck>, IQueryable<Truck>> filterFunc = q => q.Where(t => t.Code.Equals(truckCode));
            var truckListWithCodeFromDb = await _truckRepository.GetAsync(filterFunc);

            if (truckListWithCodeFromDb.Any())
            {
                return true;
            }

            return false;
        }
    }
}
