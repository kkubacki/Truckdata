namespace ErpSystem.TruckData.Contracts
{
    public class ValidationResultDto
    {
        public bool IsSuccess { get; private set; }

        public IEnumerable<ValidationErrorDto> Errors { get; private set; }

        public ValidationResultDto(bool isSuccess, IEnumerable<ValidationErrorDto> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static ValidationResultDto CreateSuccess()
        {
            return new ValidationResultDto(true, new List<ValidationErrorDto>());
        }

        public static ValidationResultDto CreateFail(IEnumerable<ValidationErrorDto> errors)
        {
            return new ValidationResultDto(false, errors);
        }
    }
}
