using ErpSystem.TruckData.Domain.Enums;
using ErpSystem.TruckData.Domain.Services;

namespace ErpSystem.TruckData.Domain.Entities
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

        public Truck(Guid id,string code, string name, TruckStatus status, string description = "")
        {
            Id = id;
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

            if (Status != newValues.Status)
            {
                ChangeStatus(newValues.Status);
            }
            
            Code = newValues.Code;
            Name = newValues.Name;
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
            if (!TruckValidationService.ValidateName(name))
            {
                throw new ArgumentException(TruckValidationService.NameErrorMessage);
            }
        }

        private void ValidateCode(string code)
        {
            if (!TruckValidationService.ValidateCode(code))
            {
                throw new ArgumentException(TruckValidationService.CodeErrorMessage);
            }
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
