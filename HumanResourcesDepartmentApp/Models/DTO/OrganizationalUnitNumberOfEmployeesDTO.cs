using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesDepartmentApp.Models
{
    public class OrganizationalUnitNumberOfEmployeesDTO
    {
        public int Id { get; set; }
        public string OrganizationalUnit { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}