using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    
    [Route("api/summary")]
    public class SummaryController : Controller
    {
        private readonly ISummaryService _summaryService;
        private readonly IClientService _clientService;

        public SummaryController(ISummaryService summaryService, IClientService clientService)
        {
            _summaryService = summaryService;
            _clientService = clientService;
        }

        [Route("user/{id}")]
        [HttpGet]
        public IActionResult Generate(Guid id)
        {
            try
            {
                var userSummaries = _summaryService.Generate(id);
                return Ok(userSummaries);
            }
            catch (Exception e)
            {
                Log.Debug($"Error generating summaries: {e}");
                return StatusCode(500, "Error generating summaries");
            }
        }
        [Route("client/{id}")]
        [HttpGet]
        public IActionResult GenerateClient(Guid id)
        {
            try
            {
                var personMatches = _clientService.FindById(id);
                if (null != personMatches)
                {
                    var client = personMatches.FirstOrDefault().Person.Clients.FirstOrDefault();
                    if (null != client)
                    {
                        var clientSummaries = _summaryService.Generate(client);
                        return Ok(clientSummaries);
                    }
                }
                return NotFound();
            }
            catch (Exception e)
            {
                Log.Debug($"Error generating summaries: {e}");
                return StatusCode(500, "Error generating summaries");
            }
        }
    }
}
