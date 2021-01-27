using HumanResourcesDepartmentApp.Models;
using HumanResourcesDepartmentApp.Models.FilterObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesDepartmentApp.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> ReadAll();
        Employee ReadById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        IEnumerable<OrganizationalUnitNumberOfEmployeesDTO> ReadOrganizationalUnitsWithNumberOfEmployees();
        IEnumerable<OrganizationalUnitAverageSalaryDTO> ReadOrganizationalUnitsWithAverageSalary(OverLimitSalaryFilter overLimitSalaryFilter);
        IEnumerable<Employee> ReadEmployeesByBirthYear(int birthYear);
        IEnumerable<Employee> ReadEmployeesByGivenSalary(BetweenLimitsSalaryFilter betweenLimitsSalaryFilter);
    }
}
