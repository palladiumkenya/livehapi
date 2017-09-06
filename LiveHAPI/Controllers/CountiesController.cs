using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    
    [Route("api/counties")]
    public class CountiesController : Controller
    {
        private readonly ICountyRepository _countyRepository;
        private readonly ILogger<CountiesController> _logger;

        public CountiesController(ICountyRepository countyRepository, ILogger<CountiesController> logger)
        {
            _countyRepository = countyRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetCounties()
        {
            try
            {
                var c = _countyRepository.GetAll().ToList();
                var counties = Mapper.Map<IEnumerable<CountyDTO>>(c);
                return Ok(counties);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"Error loading counties: {e}");
                return StatusCode(500, "Error loading counties");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetCounty(int id)
        {
    

            var c = _countyRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (null != c)
            {
                return Ok(c);
            }
            return NotFound();
        }
    }
}
