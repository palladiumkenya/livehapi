using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
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
        private readonly IPracticeRepository _configRepository;

        public SetupController(IPracticeRepository configRepository)
        {
            _configRepository = configRepository;
        }

        [HttpGet]
        public IActionResult GetFacility()
        {
            try
            {
                var practice = _configRepository.GetDefault();
                return Ok(practice);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
