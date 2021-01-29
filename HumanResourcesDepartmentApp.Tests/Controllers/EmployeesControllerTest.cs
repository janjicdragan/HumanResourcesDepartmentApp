using HumanResourcesDepartmentApp.Controllers;
using HumanResourcesDepartmentApp.Interfaces;
using HumanResourcesDepartmentApp.Models;
using HumanResourcesDepartmentApp.Models.FilterObjects;
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
    public class EmployeesControllerTest
    {
        [TestMethod]
        public void GetReturnsObjectAndOkStatusCode()
        {
            //Arrange
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(x => x.ReadById(1)).Returns(new Employee() { Id = 1, FirstAndLastName = "Pera Peric" });
            var controller = new EmployeesController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;


            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Employee>));
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            //Arrange
            var mockRepo = new Mock<IEmployeeRepository>();
            var controller = new EmployeesController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = controller.Put(1, new Employee() { Id = 2, FirstAndLastName = "Zika Zikic" });

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetReturnsSortedCollectionOfObjects()
        {
            //Arrange
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee() { Id = 1, FirstAndLastName = "Pera Peric", EmploymentYear = 2010 });
            employees.Add(new Employee() { Id = 2, FirstAndLastName = "Mika Mikic", EmploymentYear = 2011 });
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(x => x.ReadAll()).Returns(employees);
            var controller = new EmployeesController(mockRepo.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, employees.Count);
            Assert.AreEqual(result.ElementAt(0), employees.ElementAt(0));
            Assert.AreEqual(result.ElementAt(1), employees.ElementAt(1));
        }

        [TestMethod]
        public void PostReturnsCollectionOfObjectsByFilter()
        {
            //Arrange
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee() { Id = 3, FirstAndLastName = "Iva Ivic", Salary = 2000m });
            employees.Add(new Employee() { Id = 2, FirstAndLastName = "Mika Mikic", Salary = 1000m });
            BetweenLimitsSalaryFilter filter = new BetweenLimitsSalaryFilter() { MinSalary = 1000m, MaxSalary = 2000m };

            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(x => x.ReadEmployeesByGivenSalary(filter)).Returns(employees);
            var controller = new EmployeesController(mockRepo.Object);

            //Act
            var result = controller.PostFilterEmployeesBySalary(filter);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, employees.Count);
            Assert.AreEqual(result.ElementAt(0), employees.ElementAt(0));
            Assert.AreEqual(result.ElementAt(1), employees.ElementAt(1));
        }
    }
}
