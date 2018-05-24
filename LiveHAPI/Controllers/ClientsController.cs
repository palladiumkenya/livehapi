using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hangfire;
using LiveHAPI.Core.Dispatcher;
using LiveHAPI.Core.Events;
using LiveHAPI.Core.Interfaces.Handler;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.ValueObject;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{

  [Route("api/clients")]
  public class ClientsController : Controller
  {
    private readonly IClientService _clientService;
    private readonly IEncounterService _encounterService;
    private readonly IPSmartStoreService _pSmartStoreService;
    private readonly IClientSavedHandler _clientSavedHandler;
    private readonly IEncounterSavedHandler _encounterSavedHandler;
    private readonly ISubscriberSystemRepository _subscriberSystemRepository;
    private readonly SubscriberSystem _subscriberSystem;
    private readonly ISummaryService _summaryService;

    public ClientsController(IClientService clientService, IEncounterService encounterService,
      IClientSavedHandler clientSavedHandler, IEncounterSavedHandler encounterSavedHandler,
      ISubscriberSystemRepository subscriberSystemRepository, ISummaryService summaryService,
      IPSmartStoreService pSmartStoreService)
    {
      _clientService = clientService;
      _encounterService = encounterService;
      _clientSavedHandler = clientSavedHandler;
      _encounterSavedHandler = encounterSavedHandler;
      _subscriberSystemRepository = subscriberSystemRepository;
      _summaryService = summaryService;
      _pSmartStoreService = pSmartStoreService;
     _subscriberSystem = _subscriberSystemRepository.GetDefault();
    }

    [Route("name/{name}")]
    [HttpGet]
    public IActionResult FindClientNames(string name)
    {
      try
      {
        var personMatches = _clientService.SearchByName(name).ToList();
        var personData = new List<RemoteClientInfo>();

        foreach (var personMatch in personMatches)
        {
          var rc = new RemoteClientInfo();

          rc.Client = personMatch.RemoteClient.Client;

          //                    foreach (var client in personMatch.Person.Clients)
          //                    {
          //                        var es = new List<EncounterInfo>();
          //                        foreach (var clientEncounter in client.Encounters)
          //                        {
          //                            var e = Mapper.Map<EncounterInfo>(clientEncounter);
          //                            e.Obses = Mapper.Map<List<ObsInfo>>(clientEncounter.Obses.ToList());
          //                            e.ObsTestResults = Mapper.Map<List<ObsTestResultInfo>>(clientEncounter.ObsTestResults.ToList());
          //                            e.ObsFinalTestResults = Mapper.Map<List<ObsFinalTestResultInfo>>(clientEncounter.ObsFinalTestResults.ToList());
          //                            e.ObsTraceResults = Mapper.Map<List<ObsTraceResultInfo>>(clientEncounter.ObsTraceResults.ToList());
          //                            e.ObsLinkages = Mapper.Map<List<ObsLinkageInfo>>(clientEncounter.ObsLinkages.ToList());
          //                            e.ObsMemberScreenings = Mapper.Map<List<ObsMemberScreeningInfo>>(clientEncounter.ObsMemberScreenings.ToList());
          //                            e.ObsPartnerScreenings = Mapper.Map<List<ObsPartnerScreeningInfo>>(clientEncounter.ObsPartnerScreenings.ToList());
          //                            e.ObsFamilyTraceResults = Mapper.Map<List<ObsFamilyTraceResultInfo>>(clientEncounter.ObsFamilyTraceResults.ToList());
          //                            e.ObsPartnerTraceResults = Mapper.Map<List<ObsPartnerTraceResultInfo>>(clientEncounter.ObsPartnerTraceResults.ToList());
          //                            es.Add(e);
          //                        }
          //                        rc.Encounters = es;
          //                    }
          if (null != rc.Client && !rc.Client.Id.IsNullOrEmpty())
            personData.Add(rc);
        }



        return Ok(personData);
      }
      catch (Exception e)
      {
        Log.Debug($"Error searching clients: {e}");
        return StatusCode(500, "Error loading clients");
      }
    }

    [Route("id/{id}")]
    [HttpGet]
    public IActionResult FindClientIds(string id)
    {
      try
      {
        var personMatches = _clientService.SearchById(id).ToList();
        var personData = new List<RemoteClientInfo>();

        foreach (var personMatch in personMatches)
        {
          var rc = new RemoteClientInfo();

          rc.Client = personMatch.RemoteClient.Client;

          //                    foreach (var client in personMatch.Person.Clients)
          //                    {
          //                        var es = new List<EncounterInfo>();
          //                        foreach (var clientEncounter in client.Encounters)
          //                        {
          //                            var e = Mapper.Map<EncounterInfo>(clientEncounter);
          //                            e.Obses = Mapper.Map<List<ObsInfo>>(clientEncounter.Obses.ToList());
          //                            e.ObsTestResults = Mapper.Map<List<ObsTestResultInfo>>(clientEncounter.ObsTestResults.ToList());
          //                            e.ObsFinalTestResults = Mapper.Map<List<ObsFinalTestResultInfo>>(clientEncounter.ObsFinalTestResults.ToList());
          //                            e.ObsTraceResults = Mapper.Map<List<ObsTraceResultInfo>>(clientEncounter.ObsTraceResults.ToList());
          //                            e.ObsLinkages = Mapper.Map<List<ObsLinkageInfo>>(clientEncounter.ObsLinkages.ToList());
          //                            e.ObsMemberScreenings = Mapper.Map<List<ObsMemberScreeningInfo>>(clientEncounter.ObsMemberScreenings.ToList());
          //                            e.ObsPartnerScreenings = Mapper.Map<List<ObsPartnerScreeningInfo>>(clientEncounter.ObsPartnerScreenings.ToList());
          //                            e.ObsFamilyTraceResults = Mapper.Map<List<ObsFamilyTraceResultInfo>>(clientEncounter.ObsFamilyTraceResults.ToList());
          //                            e.ObsPartnerTraceResults = Mapper.Map<List<ObsPartnerTraceResultInfo>>(clientEncounter.ObsPartnerTraceResults.ToList());
          //                            es.Add(e);
          //                        }
          //                        rc.Encounters = es;
          //                    }
          if (null != rc.Client && !rc.Client.Id.IsNullOrEmpty())
            personData.Add(rc);
        }



        return Ok(personData);
      }
      catch (Exception e)
      {
        Log.Debug($"Error searching clients: {e}");
        return StatusCode(500, "Error loading clients");
      }
    }


    [Route("download/{id}")]
    [HttpGet]
    public IActionResult DownloadClient(string id)
    {
      Guid clientId = Guid.Empty;

      if (string.IsNullOrWhiteSpace(id))
        return BadRequest();

      try
      {
        clientId = new Guid(id);
      }

      catch
      {
      }

      try
      {
        var personMatches = _clientService.FindById(clientId).ToList();
        var personData = new List<RemoteClientInfo>();

        foreach (var personMatch in personMatches)
        {
          var rc = new RemoteClientInfo();

          rc.Client = personMatch.RemoteClient.Client;

          foreach (var client in personMatch.Person.Clients)
          {
            var isInHts = client.IsInState(LiveState.HtsEnrolled);
            var isPos = client.IsInState(LiveState.HtsTestedPos);
            var isFam = client.IsFamilyContact();
            var isPat = client.IsPartnerContact();

            var es = new List<EncounterInfo>();
            foreach (var clientEncounter in client.Encounters)
            {
              rc.Client.AlreadyTestedPos = false;
              var e = Mapper.Map<EncounterInfo>(clientEncounter);
              e.Obses = new List<ObsInfo>();
              e.ObsTestResults = new List<ObsTestResultInfo>();
              e.ObsFinalTestResults = new List<ObsFinalTestResultInfo>();
              e.ObsTraceResults = new List<ObsTraceResultInfo>();
              e.ObsLinkages = new List<ObsLinkageInfo>();

              e.ObsMemberScreenings = new List<ObsMemberScreeningInfo>();
              e.ObsPartnerScreenings = new List<ObsPartnerScreeningInfo>();
              e.ObsFamilyTraceResults = new List<ObsFamilyTraceResultInfo>();
              e.ObsPartnerTraceResults = new List<ObsPartnerTraceResultInfo>();
              /*
              e.Obses = Mapper.Map<List<ObsInfo>>(clientEncounter.Obses.ToList());
              e.ObsTestResults = Mapper.Map<List<ObsTestResultInfo>>(clientEncounter.ObsTestResults.ToList());
              e.ObsFinalTestResults = Mapper.Map<List<ObsFinalTestResultInfo>>(clientEncounter.ObsFinalTestResults.ToList());
              e.ObsTraceResults = Mapper.Map<List<ObsTraceResultInfo>>(clientEncounter.ObsTraceResults.ToList());
              e.ObsLinkages = Mapper.Map<List<ObsLinkageInfo>>(clientEncounter.ObsLinkages.ToList());
              e.ObsMemberScreenings = Mapper.Map<List<ObsMemberScreeningInfo>>(clientEncounter.ObsMemberScreenings.ToList());
              e.ObsPartnerScreenings = Mapper.Map<List<ObsPartnerScreeningInfo>>(clientEncounter.ObsPartnerScreenings.ToList());
              e.ObsFamilyTraceResults = Mapper.Map<List<ObsFamilyTraceResultInfo>>(clientEncounter.ObsFamilyTraceResults.ToList());
              e.ObsPartnerTraceResults = Mapper.Map<List<ObsPartnerTraceResultInfo>>(clientEncounter.ObsPartnerTraceResults.ToList());
              */


              if (isPos)
              {
                rc.Client.AlreadyTestedPos = true;
                e.ObsTraceResults =
                  Mapper.Map<List<ObsTraceResultInfo>>(clientEncounter.ObsTraceResults.ToList());
                e.ObsLinkages = Mapper.Map<List<ObsLinkageInfo>>(clientEncounter.ObsLinkages.ToList());
                if (e.EncounterTypeId == new Guid("b262fc32-852f-11e7-bb31-be2e44b06b34") ||
                    e.EncounterTypeId == new Guid("b262fda4-852f-11e7-bb31-be2e44b06b34"))
                  es.Add(e);
              }

              if (isFam)
              {
                rc.Client.IsFamilyMember = true;
                e.ObsMemberScreenings =
                  Mapper.Map<List<ObsMemberScreeningInfo>>(
                    clientEncounter.ObsMemberScreenings.ToList());
                e.ObsFamilyTraceResults =
                  Mapper.Map<List<ObsFamilyTraceResultInfo>>(clientEncounter.ObsFamilyTraceResults
                    .ToList());

                if (e.EncounterTypeId == new Guid("B262FDA4-877F-11E7-BB31-BE2E44B66B34") ||
                    e.EncounterTypeId == new Guid("B262FDA4-877F-11E7-BB31-BE2E44B67B34"))
                  es.Add(e);
              }

              if (isPat)
              {
                rc.Client.IsPartner = true;
                e.ObsPartnerScreenings =
                  Mapper.Map<List<ObsPartnerScreeningInfo>>(clientEncounter.ObsPartnerScreenings
                    .ToList());
                e.ObsPartnerTraceResults =
                  Mapper.Map<List<ObsPartnerTraceResultInfo>>(clientEncounter.ObsPartnerTraceResults
                    .ToList());

                if (e.EncounterTypeId == new Guid("B262FDA4-877F-11E7-BB31-BE2E44B68B34") ||
                    e.EncounterTypeId == new Guid("B262FDA4-877F-11E7-BB31-BE2E44B69B34"))
                  es.Add(e);
              }



            }

            rc.Encounters = es;
          }

          if (null != rc.Client && !rc.Client.Id.IsNullOrEmpty())
          {
            try
            {
              var client = personMatch.Person.Clients.FirstOrDefault();
              var summries = _summaryService.Generate(client).ToList();
              rc.Client.ClientSummaries = Mapper.Map<List<ClientSummaryInfo>>(summries);
            }
            catch (Exception e)
            {
              Log.Debug($"{e}");
            }

            personData.Add(rc);
          }
        }

        foreach (var remoteClientInfo in personData)
        {
          //

        }


        return Ok(personData.FirstOrDefault());
      }
      catch (Exception e)
      {
        Log.Debug($"Error searching clients: {e}");
        return StatusCode(500, "Error loading clients");
      }
    }

    [Route("{id}/encounters")]
    [HttpGet]
    public IActionResult FindClientEncounters(string id)
    {
      try
      {
        var personMatches = _clientService.SearchById(id).ToList();
        return Ok(personMatches);
      }
      catch (Exception e)
      {
        Log.Debug($"Error searching clients: {e}");
        return StatusCode(500, "Error loading clients");
      }
    }

    [HttpPost("demographics")]
    public IActionResult CreateClients([FromBody] ClientInfo client)
    {
      if (null == client)
        return BadRequest();

      try
      {
          BackgroundJob.Enqueue(()=>_clientService.SmartSync(client));

          //SyncEventDispatcher.Raise(new ClientSaved(client), _clientSavedHandler, _subscriberSystem);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Error($"{e}");
        return StatusCode(500, $"{e.Message}");
      }
    }

    [HttpPost("encounters")]
    public IActionResult CreateEncounters([FromBody] List<EncounterInfo> encounters)
    {
      if (null == encounters)
        return BadRequest();

      try
      {
          BackgroundJob.Enqueue(()=>_encounterService.Sync(encounters));

        //SyncEventDispatcher.Raise(new EncounterSaved(encounters), _encounterSavedHandler, _subscriberSystem);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Error($"{e}");
        return StatusCode(500, $"{e.Message}");
      }
    }

    [HttpPost("shrs")]
    public IActionResult CreateShrEncounters([FromBody] List<PSmartStoreInfo> encounters)
    {
      if (null == encounters)
        return BadRequest();

        try
        {
            BackgroundJob.Enqueue(() => _pSmartStoreService.Sync(encounters));
            //SyncEventDispatcher.Raise(new EncounterSaved(encounters), _encounterSavedHandler, _subscriberSystem);
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
