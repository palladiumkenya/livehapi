using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Dispatcher;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Shared.ValueObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    
    [Route("api/clients")]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IEncounterService _encounterService;
        private readonly IClientSavedHandler _clientSavedHandler;
        private readonly IEncounterSavedHandler _encounterSavedHandler;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(ILogger<ClientsController> logger, IClientService clientService, IEncounterService encounterService, IClientSavedHandler clientSavedHandler, IEncounterSavedHandler encounterSavedHandler)
        {
            _logger = logger;
            _clientService = clientService;
            _encounterService = encounterService;
            _clientSavedHandler = clientSavedHandler;
            _encounterSavedHandler = encounterSavedHandler;
        }

        [HttpPost("demographics")]
        public IActionResult CreateClients([FromBody] ClientInfo client)
        {
            if (null == client)
                return BadRequest();


            _logger.LogDebug(JsonConvert.SerializeObject(client));

            //            if (!ModelState.IsValid)
            //                return BadRequest(ModelState);

            try
            {
                _clientService.SyncClient(client);

                SyncEventDispatcher.Raise(new ClientSaved(client.Id),_clientSavedHandler);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Not created !");
            }
        }

        [HttpPost("encounters")]
        public IActionResult CreateEncounters( [FromBody] List<EncounterInfo> encounters)
        {
            if (null == encounters)
                return BadRequest();

            _logger.LogDebug(JsonConvert.SerializeObject(encounters));

            //            if (!ModelState.IsValid)
            //                return BadRequest(ModelState);

            try
            {
                _encounterService.Sync(encounters);

                var ids = encounters.Select(x => x.Id).ToList();

                SyncEventDispatcher.Raise(new EncounterSaved(ids), _encounterSavedHandler);

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
