using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveHAPI.Controllers
{

    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        private readonly IPracticeRepository _practiceRepository;

        public DashboardController(IPracticeRepository practiceRepository)
        {
            _practiceRepository = practiceRepository;
        }

        // GET: api/dashboard/facility
        [HttpGet("facility")]
        public virtual IActionResult GetDb()
        {

            try
            {
                var practice = _practiceRepository.GetDefault();

                if (null == practice)
                    throw new Exception("No facility");

                return Ok(practice);
            }
            catch (Exception e)
            {
                var msg = $"LiveHAPI Database connection Failed";
                return StatusCode(500, msg);
            }
        }
    }
}
