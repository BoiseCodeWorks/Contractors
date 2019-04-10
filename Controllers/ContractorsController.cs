using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using contracted.Models;
using contracted.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace contracted.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorsController : ControllerBase
    {
        private readonly ContractorsRepository _cr;
        public ContractorsController(ContractorsRepository cr)
        {
            _cr = cr;
        }

        // GET api/contractors
        [HttpGet]
        public ActionResult<IEnumerable<Contractor>> Get()
        {
            return Ok(_cr.GetALL());
        }

        // GET api/contractors/:id
        [HttpGet("{id}")]
        public ActionResult<Contractor> GetOne(string id)
        {
            return _cr.GetbyId(id);
        }


        [HttpPost]
        public ActionResult<Contractor> Create([FromBody]Contractor newCon)
        {
            return _cr.CreateContractor(newCon);
        }

        [HttpPut("{id}")]
        public ActionResult<Contractor> Edit(string id, [FromBody] Contractor editCon)
        {
            return _cr.EditContractor(id, editCon);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(string id)
        {
            bool wasSuccessful = _cr.DeleteContractor(id);
            if (wasSuccessful)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("{conId}/jobs/{jobId}")]
        public ActionResult<string> AddContractorToJob(string conId, int jobId)
        {
            return _cr.AddConToJob(conId, jobId);
        }


        //THIS SHOULD BE IN THE JOB CONTROLLER AS "{jobId}/contractors"
        [HttpGet("{jobId}/jobs")]
        public ActionResult<IEnumerable<Contractor>> GetContractorsByJobId(int jobId)
        {
            return Ok(_cr.GetConsByJobId(jobId));
        }
    }
}
