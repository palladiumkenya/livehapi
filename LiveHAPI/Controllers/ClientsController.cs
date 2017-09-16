using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model;
using LiveHAPI.Model;
using LiveHAPI.Shared.ValueObject;
using LiveHAPI.Shared.ValueObject.Meta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Remotion.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    
    [Route("api/clients")]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IEncounterService _encounterService;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(ILogger<ClientsController> logger, IClientService clientService, IEncounterService encounterService)
        {
            _logger = logger;
            _clientService = clientService;
            _encounterService = encounterService;
        }

        [HttpPost("demographics")]
        public IActionResult CreateClients(int countyId, [FromBody] ClientInfo client)
        {
            if (null == client)
                return BadRequest();

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

            try
            {
                _clientService.SyncClient(client);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Not created !");
            }
        }

        [HttpPost("encounters")]
        public IActionResult CreateEncounters(int countyId, [FromBody] List<EncounterInfo> encounters)
        {
            if (null == encounters)
                return BadRequest();

            //            if (!ModelState.IsValid)
            //                return BadRequest(ModelState);

            try
            {
                _encounterService.Sync(encounters);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Not created !");
            }
        }
    }
}
