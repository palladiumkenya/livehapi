using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Dispatcher;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.ValueObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

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
        private readonly ISubscriberSystemRepository _subscriberSystemRepository;
        private readonly SubscriberSystem _subscriberSystem;

        public ClientsController(IClientService clientService, IEncounterService encounterService, IClientSavedHandler clientSavedHandler, IEncounterSavedHandler encounterSavedHandler, ISubscriberSystemRepository subscriberSystemRepository)
        {
            _clientService = clientService;
            _encounterService = encounterService;
            _clientSavedHandler = clientSavedHandler;
            _encounterSavedHandler = encounterSavedHandler;
            _subscriberSystemRepository = subscriberSystemRepository;

            _subscriberSystem = _subscriberSystemRepository.GetDefault();
        }

        [HttpPost("demographics")]
        public IActionResult CreateClients([FromBody] ClientInfo client)
        {
            if (null == client)
                return BadRequest();
            


            try
            {
                _clientService.SyncClient(client);
                
                SyncEventDispatcher.Raise(new ClientSaved(client),_clientSavedHandler, _subscriberSystem);

                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpPost("encounters")]
        public IActionResult CreateEncounters( [FromBody] List<EncounterInfo> encounters)
        {
            if (null == encounters)
                return BadRequest();

            try
            {
                _encounterService.Sync(encounters);


                SyncEventDispatcher.Raise(new EncounterSaved(encounters),  _encounterSavedHandler, _subscriberSystem);

                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
