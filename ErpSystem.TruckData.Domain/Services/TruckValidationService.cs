
using ErpSystem.TruckData.Domain.Enums;

namespace ErpSystem.TruckData.Domain.Services
{
    public static class TruckValidationService
    {
        public static bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return true;
        }

        public static bool ValidateStatusString(string statusString)
        {
            if (Enum.TryParse<TruckStatus>(statusString, out var truckStatus))
            {
                return true;
            }

            return false;
        }

        public static bool ValidateCode(string code)
        {
            if (!IsAlphaNumeric(code))
            {
                return false;
            }

            return true;
        }

        private static bool IsAlphaNumeric(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsLetterOrDigit);
        }

        public static string CodeErrorMessage = "must be alphanumeric.";

        public static string NameErrorMessage = "must have value.";

        public static string StatusValidationError = "must be one of  'Out of Service','Loading','To Job','At Job','Returning'";
    }
}
