using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveHAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Manager")]
    public class ManagerController : Controller
    {
        private readonly ISyncManagerService _managerService;

        public ManagerController(ISyncManagerService managerService)
        {
            _managerService = managerService;
        }


        [HttpGet("Stats")]
        public IActionResult GetStats()
        {
            try
            {
                var stats = _managerService.GetStats();
                return Ok(stats);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpGet("Errors")]
        public IActionResult GetErrors()
        {
            try
            {
                var clientStages = _managerService.GetSyncErrorClients();
                return Ok(clientStages);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpPost("ResendAll")]
        public async Task<IActionResult> ResendAllAsync()
        {
            try
            {
               await _managerService.ResendAll();

                var clientStages = _managerService.GetSyncErrorClients();
                return Ok(clientStages);
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpPost("Resend")]
        public async Task<IActionResult> CreateEncounters([FromBody] List<Guid> clientIds)
        {
            if (!clientIds.Any())
                return BadRequest();

            try
            {
                await _managerService.Resend(clientIds);

                var clientStages = _managerService.GetSyncErrorClients();
                return Ok(clientStages);
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }


    }
}
