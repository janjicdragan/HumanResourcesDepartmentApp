using HumanResourcesDepartmentApp.Interfaces;
using HumanResourcesDepartmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HumanResourcesDepartmentApp.Repository
{
    public class OrganizationalUnitRepository : IOrganizationalUnitRepository, IDisposable
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

        public IEnumerable<OrganizationalUnit> ReadAll()
        {
            return db.OrganizationalUnits;
        }

        public OrganizationalUnit ReadById(int id)
        {
            return db.OrganizationalUnits.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<OrganizationalUnit> ReadTradition()
        {
            List<OrganizationalUnit> result = new List<OrganizationalUnit>();
            IEnumerable<OrganizationalUnit> data = db.OrganizationalUnits.OrderBy(o => o.YearOfFoundation);

            result.Add(data.FirstOrDefault());
            result.Add(data.LastOrDefault());

            return result.AsEnumerable();
        }
    }
}