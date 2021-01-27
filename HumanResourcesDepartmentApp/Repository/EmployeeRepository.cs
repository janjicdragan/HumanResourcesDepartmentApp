using HumanResourcesDepartmentApp.Interfaces;
using HumanResourcesDepartmentApp.Models;
using HumanResourcesDepartmentApp.Models.FilterObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HumanResourcesDepartmentApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        ApplicationDbContext db = new ApplicationDbContext();

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Employee> ReadAll()
        {
            return db.Employees.Include(e => e.OrganizationalUnit);
        }

        public Employee ReadById(int id)
        {
            return db.Employees.Include(e => e.OrganizationalUnit).FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void Update(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Employee employee)
        {
            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        public IEnumerable<OrganizationalUnitNumberOfEmployeesDTO> ReadOrganizationalUnitsWithNumberOfEmployees()
        {
            return db.Employees
                .Include(e => e.OrganizationalUnit)
                .GroupBy(e => e.OrganizationalUnit, e => e.OrganizationalUnitId,
                (organizationalUnit, numberOfEmployees) => new OrganizationalUnitNumberOfEmployeesDTO()
                {
                    Id = organizationalUnit.Id,
                    OrganizationalUnit = organizationalUnit.Name,
                    NumberOfEmployees = numberOfEmployees.Count()
                }).OrderByDescending(o => o.NumberOfEmployees);
        }

        public IEnumerable<OrganizationalUnitAverageSalaryDTO> ReadOrganizationalUnitsWithAverageSalary(OverLimitSalaryFilter overLimitSalaryFilter)
        {
            return db.Employees
                .Include(e => e.OrganizationalUnit)
                .GroupBy(e => e.OrganizationalUnit, e => e.Salary,
                (organizationalUnit, salary) => new OrganizationalUnitAverageSalaryDTO()
                {
                    Id = organizationalUnit.Id,
                    OrganizationalUnit = organizationalUnit.Name,
                    AverageSalary = salary.Average()
                }).Where(o => o.AverageSalary > overLimitSalaryFilter.Limit)
                .OrderBy(o => o.AverageSalary);

        }

        public IEnumerable<Employee> ReadEmployeesByBirthYear(int birthYear)
        {
            return db.Employees
                .Include(e => e.OrganizationalUnit)
                .Where(e => e.BirthYear > birthYear)
                .OrderBy(e => e.BirthYear);
        }

        public IEnumerable<Employee> ReadEmployeesByGivenSalary(BetweenLimitsSalaryFilter betweenLimitsSalaryFilter)
        {
            return db.Employees
                .Include(e => e.OrganizationalUnit)
                .Where(e => e.Salary >= betweenLimitsSalaryFilter.MinSalary && e.Salary <= betweenLimitsSalaryFilter.MaxSalary)
                .OrderByDescending(e => e.Salary);
        }
    }
}