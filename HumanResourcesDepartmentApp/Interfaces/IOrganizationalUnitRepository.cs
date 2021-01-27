using HumanResourcesDepartmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesDepartmentApp.Interfaces
{
    public interface IOrganizationalUnitRepository
    {
        IEnumerable<OrganizationalUnit> ReadAll();
        OrganizationalUnit ReadById(int id);
        IEnumerable<OrganizationalUnit> ReadTradition();
    }
}
