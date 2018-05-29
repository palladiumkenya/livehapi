using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiveHAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Sync")]
    public class SyncController : Controller
    {
        [HttpGet]
        public IActionResult GetFacility()
        {
            try
            {
                //var practice = _practiceRepository.GetDefault();
                //return Ok(practice);
            }
            catch (Exception e)
            {
                //Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
