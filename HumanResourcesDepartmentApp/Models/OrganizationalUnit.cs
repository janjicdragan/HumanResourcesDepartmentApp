using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesDepartmentApp.Models
{
    public class OrganizationalUnit
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Range(2010,2019)]
        public int YearOfFoundation { get; set; }
    }
}