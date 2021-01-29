namespace HumanResourcesDepartmentApp.Migrations
{
    using HumanResourcesDepartmentApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HumanResourcesDepartmentApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HumanResourcesDepartmentApp.Models.ApplicationDbContext context)
        {
            context.OrganizationalUnits.AddOrUpdate(x => x.Id,
                new OrganizationalUnit() { Id = 1, Name = "Administration", YearOfFoundation = 2010 },
                new OrganizationalUnit() { Id = 2, Name = "Accounting", YearOfFoundation = 2012 },
                new OrganizationalUnit() { Id = 3, Name = "Development", YearOfFoundation = 2013 }
                );

            context.Employees.AddOrUpdate(x => x.Id,
                new Employee() { Id = 1, FirstAndLastName = "Pera Peric", Role = "Director", BirthYear = 1980, EmploymentYear = 2010, Salary = 3000m, OrganizationalUnitId = 1 },
                new Employee() { Id = 2, FirstAndLastName = "Mika Mikic", Role = "Secretary", BirthYear = 1985, EmploymentYear = 2011, Salary = 1000m, OrganizationalUnitId = 1 },
                new Employee() { Id = 3, FirstAndLastName = "Iva Ivic", Role = "Accountant", BirthYear = 1981, EmploymentYear = 2012, Salary = 2000m, OrganizationalUnitId = 2 },
                new Employee() { Id = 4, FirstAndLastName = "Zika Zikic", Role = "Engineer", BirthYear = 1982, EmploymentYear = 2013, Salary = 2500m, OrganizationalUnitId = 3 },
                new Employee() { Id = 5, FirstAndLastName = "Ana Anic", Role = "Engineer", BirthYear = 1984, EmploymentYear = 2014, Salary = 2500m, OrganizationalUnitId = 3 }
                );
        }
    }
}
