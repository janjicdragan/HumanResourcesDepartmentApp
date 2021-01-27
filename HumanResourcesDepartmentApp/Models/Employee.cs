﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesDepartmentApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string FirstAndLastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Role { get; set; }
        [Range(1960, 1999)]
        public int? BirthYear { get; set; }
        [Required]
        [Range(2010, 2020)]
        public int EmploymentYear { get; set; }
        [Required]
        [Range(251d, 9999d)]
        public decimal Salary { get; set; }
        public OrganizationalUnit OrganizationalUnit { get; set; }
        public int OrganizationalUnitId { get; set; }
    }
}