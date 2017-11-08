using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiveHAPI.Core.Dispatcher;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.ValueObject;
using LiveHAPI.Shared.ValueObject.Meta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    
    [Route("api/cohorts")]
    public class CohortsController : Controller
    {
        private readonly ISubscriberSystemRepository _subscriberSystemRepository;
        private readonly SubscriberSystem _subscriberSystem;

        public CohortsController(IClientService clientService, IEncounterService encounterService, IClientSavedHandler clientSavedHandler, IEncounterSavedHandler encounterSavedHandler, ISubscriberSystemRepository subscriberSystemRepository)
        {
            _subscriberSystemRepository = subscriberSystemRepository;
            _subscriberSystem = _subscriberSystemRepository.GetDefault();
        }

        [Route("lists")]
        [HttpGet]
        public IActionResult GetLists()
        {
            try
            {
                if(null==_subscriberSystem)
                    throw new Exception("Server Systems not configured");

                var cohorts=  Mapper.Map<List<CohortInfo>>(_subscriberSystem.Cohorts.ToList());
                return Ok(cohorts);
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
