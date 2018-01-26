using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/setup")]
    public class SetupController : Controller
    {
        private readonly IConfigRepository _configRepository;

        public SetupController(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _configRepository.GetUsers().ToList();
                return Ok(users);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetFacilities()
        {
            try
            {
                var locations = _configRepository.GetLocations().ToList();
                return Ok(locations);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
