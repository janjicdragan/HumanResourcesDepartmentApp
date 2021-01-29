using HumanResourcesDepartmentApp.Interfaces;
using HumanResourcesDepartmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HumanResourcesDepartmentApp.Controllers
{
    public class OrganizationalUnitsController : ApiController
    {
        private IOrganizationalUnitRepository _repository { get; set; }

        public OrganizationalUnitsController(IOrganizationalUnitRepository repository)
        {
            _repository = repository;
        }

        //GET api/organizationalunits
        [ResponseType(typeof(IEnumerable<OrganizationalUnit>))]
        public IEnumerable<OrganizationalUnit> Get()
        {
            return _repository.ReadAll();
        }

        //GET api/organizationalunits/1
        [ResponseType(typeof(OrganizationalUnit))]
        public IHttpActionResult Get(int id) {

            OrganizationalUnit organizationalUnit = _repository.ReadById(id);
            
            if(organizationalUnit == null)
            {
                return NotFound();
            }

            return Ok(organizationalUnit);

        }

        //GET api/tradition
        [Route("api/tradition")]
        [ResponseType(typeof(IEnumerable<OrganizationalUnit>))]
        public IEnumerable<OrganizationalUnit> GetTradition()
        {
            return _repository.ReadTradition();
        }
    }
}
