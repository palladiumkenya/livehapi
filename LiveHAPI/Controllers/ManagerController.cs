using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Shared.Custom;
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

        [HttpGet("Stats/{id}")]
        public IActionResult ProviderStats(Guid id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();
            try
            {
                var stats = _managerService.GetStats(id);
                return Ok(stats);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpGet("ErrorsCount")]
        public IActionResult GetErrorsCount()
        {
            try
            {
                var count = _managerService.GetSyncErrorClientsCount();
                return Ok(count);
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
                var clientStages = _managerService.GetSyncErrorClients().ToList();
                return Ok(clientStages);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpGet("StagedCount")]
        public IActionResult GetStagedCount()
        {
            try
            {
                var count = _managerService.GetSyncStagedCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpGet("Staged")]
        public IActionResult GetStaged()
        {
            try
            {
                var clientStages = _managerService.GetSyncStagedClients().ToList();
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

                var clientStages = _managerService.GetSyncErrorClients().ToList();
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

                var clientStages = _managerService.GetSyncErrorClients().ToList();
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
