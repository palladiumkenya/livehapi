using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace LiveHAPI.Controllers
{
    [Route("api/counties")]
    public class SubCountiesController:Controller
    {
        [HttpGet("{countyId}/SubCounties")]
        public IActionResult GetSubCounties(int countyId)
        {
            var counties = new List<County>()
            {
                new County(47,"Nairobi"),
                new County(1,"Mombasa")
            };

            counties[0].AddSubCounty(new SubCounty("Kibera",1));
            counties[0].AddSubCounty(new SubCounty("Langata", 2));

            counties[1].AddSubCounty(new SubCounty("Town", 1));

            var c = counties.FirstOrDefault(x => x.Id == countyId);

            if (null != c)
            {
                return Ok(c);
            }

            return NotFound();
        }
    }
}