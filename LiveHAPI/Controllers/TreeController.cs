using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveHAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Tree")]
    public class TreeController : Controller
    {
        private readonly ITreeService _treeService;

        public TreeController(ITreeService treeService)
        {
            _treeService = treeService;
        }

        // GET
        public IActionResult Get()
        {
            try
            {
                var m = _treeService.GetAll().ToList();
                return Ok(m);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, "Error loading");
            }
        }
    }
}
