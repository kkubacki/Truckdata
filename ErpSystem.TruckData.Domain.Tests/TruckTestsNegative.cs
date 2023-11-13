using ErpSystem.TruckData.Domain.Entities;
using ErpSystem.TruckData.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErpSystemTruckData.Domain.Tests
{
    [TestClass]
    public class TruckTestsNegative
    {
        [TestMethod]
        public void InvalidStatusTransition_FromLoadingToOutOfService_ThrowsException()
        {
            // Arrange
            var truck = new Truck("ABC123", "Truck1", TruckStatus.Loading);

            // Act and Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                truck.ChangeStatus(TruckStatus.OutOfService);
            });
        }

        [TestMethod]
        public void InvalidStatusTransition_FromAtJobToLoading_ThrowsException()
        {
            // Arrange
            var truck = new Truck("ABC123", "Truck1", TruckStatus.AtJob);

            // Act and Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                truck.ChangeStatus(TruckStatus.Loading);
            });
        }

        [TestMethod]
        public void InvalidStatusTransition_FromReturningToAtJob_ThrowsException()
        {
            // Arrange
            var truck = new Truck("ABC123", "Truck1", TruckStatus.Returning);

            // Act and Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                truck.ChangeStatus(TruckStatus.AtJob);
            });
        }

        [TestMethod]
        public void InvalidStatusTransition_FromToJobToReturning_ThrowsException()
        {
            // Arrange
            var truck = new Truck("ABC123", "Truck1", TruckStatus.ToJob);

            // Act and Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                truck.ChangeStatus(TruckStatus.Returning);
            });
        }

        [TestMethod]
        public void Constructor_InvalidCode_ThrowsArgumentException()
        {
            // Arrange
            string invalidCode = "Invalid Code";
            string name = "Truck1";
            TruckStatus status = TruckStatus.Loading;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                var truck = new Truck(invalidCode, name, status);
            });
        }

        [TestMethod]
        public void Constructor_NullOrEmptyName_ThrowsArgumentException()
        {
            // Arrange
            string code = "ABC123";
            string emptyName = "";
            TruckStatus status = TruckStatus.Loading;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                var truck = new Truck(code, emptyName, status);
            });

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var truck = new Truck(code, "", status);
            });
        }
    }
}