using System.Data;
using System.Xml.Linq;
using TruckData.Domain.Enums;

namespace TruckData.Domain.Entities
{
    public class Truck
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public TruckStatus Status { get; private set; }
        public string Description { get; private set; }

        public Truck(string code, string name, TruckStatus status, string description = "")
        {
            Id = Guid.NewGuid();
            ValidateCode(code);
            ValidateName(name);
            Code = code;
            Name = name;
            Status = status;
            Description = description;
        }

        public void Update(Truck newValues)
        {
            ValidateCode(newValues.Code);
            ValidateName(newValues.Name);
            ChangeStatus(newValues.Status);
            Code = Code;
            Name = Name;
            Status = newValues.Status;
            Description = newValues.Description;
        }

        public void ChangeStatus(TruckStatus newStatus)
        {           
            if (IsValidStatusTransition(Status, newStatus))
            {
                Status = newStatus;
            }
            else
            {
                throw new InvalidOperationException($"You cannot change from {Status} to {newStatus}");
            }
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null, empty, or whitespace.");
            }
        }

        private void ValidateCode(string code)
        {
            if (!IsAlphaNumeric(code))
            {
                throw new ArgumentException("Code must be alphanumeric.");
            }
        }

        private bool IsAlphaNumeric(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsLetterOrDigit);
        }

        private bool IsValidStatusTransition(TruckStatus currentStatus, TruckStatus newStatus)
        {

            switch (currentStatus)
            {
                case TruckStatus.OutOfService:
                    return true;
                case TruckStatus.Loading:
                    return newStatus == TruckStatus.ToJob;
                case TruckStatus.ToJob:
                    return newStatus == TruckStatus.AtJob;
                case TruckStatus.AtJob:
                    return newStatus == TruckStatus.Returning;
                case TruckStatus.Returning:
                    return newStatus == TruckStatus.Loading;
                default:
                    return false;
            }
        }
    }
}
