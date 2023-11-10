using Microsoft.VisualStudio.TestTools.UnitTesting;
using TruckData.Domain.Entities;
using TruckData.Domain.Enums;

namespace TruckData.Domain.Tests
{
    [TestClass]
    public class TruckTests
    {
        [TestMethod]
        public void ValidStatusTransition_FromLoadingToToJob_Succeeds()
        {
            // Arrange
            var truck = new Truck("ABC123", "Truck1", TruckStatus.Loading);

            // Act
            truck.ChangeStatus(TruckStatus.ToJob);

            // Assert
            Assert.AreEqual(TruckStatus.ToJob, truck.Status);
        }

        [TestMethod]
        public void ValidStatusTransition_FromToJobToAtJob_Succeeds()
        {
            // Arrange
            var truck = new Truck("ABC123", "Truck1", TruckStatus.ToJob);

            // Act
            truck.ChangeStatus(TruckStatus.AtJob);

            // Assert
            Assert.AreEqual(TruckStatus.AtJob, truck.Status);
        }

        [TestMethod]
        public void ValidStatusTransition_FromAtJobToReturning_Succeeds()
        {
            // Arrange
            var truck = new Truck("ABC123", "Truck1", TruckStatus.AtJob);

            // Act
            truck.ChangeStatus(TruckStatus.Returning);

            // Assert
            Assert.AreEqual(TruckStatus.Returning, truck.Status);
        }

        [TestMethod]
        public void ValidStatusTransition_FromReturningToLoading_Succeeds()
        {
            // Arrange
            var truck = new Truck("ABC123", "Truck1", TruckStatus.Returning);

            // Act
            truck.ChangeStatus(TruckStatus.Loading);

            // Assert
            Assert.AreEqual(TruckStatus.Loading, truck.Status);
        }

        [TestMethod]
        public void Constructor_ValidArguments_SetsProperties()
        {
            // Arrange
            string code = "ABC123";
            string name = "Truck1";
            TruckStatus status = TruckStatus.Loading;

            // Act
            var truck = new Truck(code, name, status);

            // Assert
            Assert.AreEqual(code, truck.Code);
            Assert.AreEqual(name, truck.Name);
            Assert.AreEqual(status, truck.Status);
            Assert.AreEqual(string.Empty, truck.Description);
        }
    }
}
