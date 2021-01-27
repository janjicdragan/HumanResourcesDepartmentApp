using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesDepartmentApp.Models.FilterObjects
{
    public class BetweenLimitsSalaryFilter
    {
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
    }
}