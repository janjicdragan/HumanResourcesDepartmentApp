using HumanResourcesDepartmentApp.Interfaces;
using HumanResourcesDepartmentApp.Models;
using HumanResourcesDepartmentApp.Models.FilterObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace HumanResourcesDepartmentApp.Controllers
{
  
    public class EmployeesController : ApiController
    {
        public IEmployeeRepository _repository { get; set; }

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }


        //GET api/employees
        [ResponseType(typeof(IEnumerable<Employee>))]
        public IEnumerable<Employee> Get()
        {
            return _repository.ReadAll();
        }

        //GET api/employees/1
        [Authorize]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult Get(int id)
        {
            Employee employee = _repository.ReadById(id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        //POST api/employees
        [Authorize]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _repository.Add(employee);

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, _repository.ReadById(employee.Id));
        }

        //PUT api/employees/1
        [Authorize]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult Put(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != employee.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(employee);
            }
            catch
            {

                return BadRequest();
            }

            return Ok(_repository.ReadById(employee.Id));
        }

        //DELETE api/employees/1
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            Employee employee = _repository.ReadById(id);

            if (employee == null)
            {
                return NotFound();
            }

            _repository.Delete(employee);

            return Ok();
        }

        //GET api/unitemployees
        [Route("api/unitemployees")]
        [ResponseType(typeof(IEnumerable<OrganizationalUnitNumberOfEmployeesDTO>))]
        public IEnumerable<OrganizationalUnitNumberOfEmployeesDTO> GetUnitWithNumberOfEmployees()
        {
            return _repository.ReadOrganizationalUnitsWithNumberOfEmployees();
        }

        //GET api/employees?birthYear=1
        [Authorize]
        [ResponseType(typeof(IEnumerable<Employee>))]
        public IEnumerable<Employee> GetEmployeesByBirthYear(int birthYear)
        {
            return _repository.ReadEmployeesByBirthYear(birthYear);
        }

        //POST api/employeesfilter
        [Authorize]
        [Route("api/employeesfilter")]
        [ResponseType(typeof(IEnumerable<Employee>))]
        public IEnumerable<Employee> PostFilterEmployeesBySalary(BetweenLimitsSalaryFilter filter)
        {
            return _repository.ReadEmployeesByGivenSalary(filter);
        }

        //POST api/unitsfilter
        [Route("api/unitsfilter")]
        [ResponseType(typeof(IEnumerable<OrganizationalUnitAverageSalaryDTO>))]
        public IEnumerable<OrganizationalUnitAverageSalaryDTO> PostFilterOrganizationalUnitsBySalary(OverLimitSalaryFilter filter)
        {
            return _repository.ReadOrganizationalUnitsWithAverageSalary(filter);
        }
    }
}
