using System;
using Microsoft.AspNetCore.Mvc;
using SweetnSaltyBusiness;
using SweetnSaltyModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SweetnSaltyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SweetnSaltyController : Controller
    {
        private readonly ISweetnSaltyBusinessClass _businessClass;
        //constructor
        public SweetnSaltyController(ISweetnSaltyBusinessClass ISweetnSaltyBusinessClass)
        {
            this._businessClass = ISweetnSaltyBusinessClass;
        }


        [HttpPost]
        [Route("postaflavor/{flavor}")]
        public async Task<ActionResult<Flavor>> PostFlavor(string flavor)
        {
            Flavor f = await this._businessClass.PostFlavor(flavor);

            if (f != null)
                return Created($"http://5001/sweetnsalty/postaflavor/{f.FlavorId}", f);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("postaperson/{fname}/{lname}")]
        public async Task<ActionResult<Person>> PostPerson(string fname, string lname)
        {
            Person p = await this._businessClass.PostPerson(fname, lname);

            if (p != null)
                return Created($"http://5001/sweetnsalty/postaperson/{p.PersonId}", p);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("getaperson/{fname}/{lname}")]
        public async Task<ActionResult<Person>> GetPerson(string fname, string lname)
        {
            Person p = await this._businessClass.GetPerson(fname, lname);

            if (p != null)
                return Ok(p);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("getapersonandflavors/{id}")]
        public async Task<ActionResult<Person>> GetPersonAndFlavors(int id)
        {
            Person p = await this._businessClass.GetPersonAndFlavors(id);

            if (p != null)
                return Ok(p);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("getlistofflavors")]
        public async Task<ActionResult<List<Flavor>>> GetAllFlavors()
        {
            List<Flavor> flavors = await this._businessClass.GetAllFlavors();

            if (flavors.Count > 0)
                return Ok(flavors);
            else
                return NotFound();
        }
    }
}
