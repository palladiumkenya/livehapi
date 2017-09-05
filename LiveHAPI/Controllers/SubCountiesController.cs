﻿using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace LiveHAPI.Controllers
{
    [Route("api/counties")]
    public class SubCountiesController:Controller
    {
        private readonly ILogger<SubCountiesController> _logger;

        public SubCountiesController(ILogger<SubCountiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{countyId}/subcounties")]
        public IActionResult GetSubCounties(int countyId)
        {
            var c = CountiesData().FirstOrDefault(x => x.Id == countyId);

            if (null != c)
            {
                _logger.LogError($"county {countyId} Not Found");
                return Ok(c);
            }

            return NotFound();
        }

        [HttpGet("{countyId}/subcounties/{code}",Name = "GetSubCounty")]
        public IActionResult GetSubCounty(int countyId,int code)
        {
            var c = CountiesData().FirstOrDefault(x => x.Id == countyId);

            if (null == c)
                return NotFound();

            var sc = c.SubCounties.FirstOrDefault(x => x.Code == code);

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

            var county = CountiesData().FirstOrDefault(x => x.Id == countyId);

            if (null == county)
                return NotFound();

            county.AddSubCounty(subCounty);

            return CreatedAtRoute("GetSubCounty",new
            { countyId = countyId ,code=subCounty.Code},subCounty
                );

        }

        [HttpPut("{countyId}/subcounties")]
        public IActionResult UpdateSubCounty(int countyId, [FromBody] SubCounty subCounty)
        {
            if (null == subCounty)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var county = CountiesData().FirstOrDefault(x => x.Id == countyId);

            if (null == county)
                return NotFound();


            var sc = county.SubCounties.FirstOrDefault(x => x.Code == subCounty.Code);

            if (null == sc)
                return NotFound();

            sc.Name = subCounty.Name;

            return NoContent();

        }


        [HttpDelete("{countyId}/subcounties/{code}")]
        public IActionResult DeleteSubCounty(int countyId, int code)
        {
            var c = CountiesData().FirstOrDefault(x => x.Id == countyId);

            if (null == c)
                return NotFound();

            var sc = c.SubCounties.FirstOrDefault(x => x.Code == code);

            if (null == sc)
                return NotFound();

            c.SubCounties.Remove(sc);

            return NoContent();
        }


        private static List<County> CountiesData()
        {
             var counties = new List<County>()
            {
                new County(1,"Nairobi"),
                new County(2,"Mombasa")
            };
            
            counties[0].AddSubCounty(new SubCounty("Kibera", 1,1));
            counties[0].AddSubCounty(new SubCounty("Langata", 2,1));
            counties[1].AddSubCounty(new SubCounty("Town", 3,2));

            return counties;
        }
    }
}