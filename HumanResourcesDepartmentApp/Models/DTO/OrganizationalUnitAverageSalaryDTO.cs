using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesDepartmentApp.Models
{
    public class OrganizationalUnitAverageSalaryDTO
    {
        public int Id { get; set; }
        public string OrganizationalUnit { get; set; }
        public decimal AverageSalary { get; set; }
    }
}