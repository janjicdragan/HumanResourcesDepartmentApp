using HumanResourcesDepartmentApp.Controllers;
using HumanResourcesDepartmentApp.Interfaces;
using HumanResourcesDepartmentApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace HumanResourcesDepartmentApp.Tests.Controllers
{
    [TestClass]
    public class OrganizationalUnitsControllerTest
    {
        [TestMethod]
        public void GetReturnsObjectAndOkStatusCode()
        {
            //Arrange
            var mockRepo = new Mock<IOrganizationalUnitRepository>();
            mockRepo.Setup(x => x.ReadById(1)).Returns(new OrganizationalUnit() { Id = 1, Name = "Administration" });
            var controller = new OrganizationalUnitsController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<OrganizationalUnit>;


            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<OrganizationalUnit>));
        }

        [TestMethod]
        public void GetReturnsNotFound()
        {
            //Arrange
            var mockRepo = new Mock<IOrganizationalUnitRepository>();
            var controller = new OrganizationalUnitsController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = controller.Get(1);


            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetReturnsCollectionOfObjects()
        {
            //Arrange
            List<OrganizationalUnit> units = new List<OrganizationalUnit>();
            units.Add(new OrganizationalUnit() { Id = 1, Name = "Administration", YearOfFoundation = 2010 });
            units.Add(new OrganizationalUnit() { Id = 2, Name = "Accounting", YearOfFoundation = 2012 });
            var mockRepo = new Mock<IOrganizationalUnitRepository>();
            mockRepo.Setup(x => x.ReadAll()).Returns(units);
            var controller = new OrganizationalUnitsController(mockRepo.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, units.Count);
        }


        [TestMethod]
        public void GetReturnsCollectionOfTwoObjectsWithTheHighestAndTheLowestYearOfFoundation()
        {
            //Arrange
            List<OrganizationalUnit> units = new List<OrganizationalUnit>();
            units.Add(new OrganizationalUnit() { Id = 1, Name = "Administration", YearOfFoundation = 2010 });
            units.Add(new OrganizationalUnit() { Id = 3, Name = "Development", YearOfFoundation = 2013 });
            var mockRepo = new Mock<IOrganizationalUnitRepository>();
            mockRepo.Setup(x => x.ReadTradition()).Returns(units);
            var controller = new OrganizationalUnitsController(mockRepo.Object);

            //Act
            var result = controller.GetTradition();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, units.Count);
            Assert.AreEqual(result.ToList().Count, 2);
            Assert.AreEqual(result.ElementAt(0), units.ElementAt(0));
            Assert.AreEqual(result.ElementAt(1), units.ElementAt(1));
        }

    }
}
