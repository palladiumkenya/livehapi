using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace LiveHAPI.Controllers
{
    [Route("api/counties")]
    public class SubCountiesController:Controller
    {
        private readonly ISubCountyRepository _subCountyRepository;
        
        private readonly ILogger<SubCountiesController> _logger;
        private readonly IMailService _mailService;
        public SubCountiesController(ILogger<SubCountiesController> logger, IMailService mailService, ISubCountyRepository subCountyRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _subCountyRepository = subCountyRepository;
        }

        [HttpGet("{countyId}/subcounties")]
        public IActionResult GetSubCounties(int countyId)
        {
            var c = _subCountyRepository.GetByCounty(countyId);

            if (null != c)
            {
                return Ok(c);
            }
            _logger.LogError($"county {countyId} Not Found!");

            return NotFound();
        }

        /*
        [HttpGet("{countyId}/subcounties/{code}",Name = "GetSubCounty")]
        public IActionResult GetSubCounty(int countyId,int code)
        {

            var sc = _subCountyRepository.GetByCounty(countyId).FirstOrDefault(x => x.Code == code);

            if (null == sc)
                return NotFound();

            return Ok(sc);
        }

        [HttpPost("{countyId}/subcounties")]
        public IActionResult CreateSubCounty(int countyId, [FromBody] SubCounty subCounty)
        {
            if (null == subCounty)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _subCountyRepository.Save(subCounty);

                return CreatedAtRoute("GetSubCounty", new
                        { countyId = countyId, code = subCounty.Code }, subCounty
                );
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Not created !");
            }


        }

        [HttpPut("{countyId}/subcounties")]
        public IActionResult UpdateSubCounty(int countyId, [FromBody] SubCounty subCounty)
        {
            if (null == subCounty)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var sc = _subCountyRepository.GetByCounty(countyId).FirstOrDefault(x => x.Code == subCounty.Code);

            if (null == sc)
                return NotFound();

            try
            {
                sc.Name = subCounty.Name;
                _subCountyRepository.Update(subCounty);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Not updated !");
            }
            

            

        }
        */

        [HttpDelete("{countyId}/subcounties/{code}")]
        public IActionResult DeleteSubCounty(Guid id)
        {
            
            try
            {
                _subCountyRepository.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Not deleted !");
            }
            
        }
    }
}