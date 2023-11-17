namespace ErpSystem.TruckData.Contracts
{
    public class ValidationErrorDto
    {
        public string FieldName { get; private set; }

        public string Message { get; private set; }

        public ValidationErrorDto(string fieldName, string message)
        {
            FieldName = fieldName;
            Message = message;
        }
    }
}
