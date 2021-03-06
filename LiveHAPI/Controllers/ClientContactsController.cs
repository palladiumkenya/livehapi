using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Shared.ValueObject;
using LiveHAPI.Shared.ValueObject.Meta;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveHAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/ClientContacts")]
    public class ClientContactsController : Controller
    {
        private readonly IClientContactNetworkRepository _contactNetworkRepository;

        public ClientContactsController(IClientContactNetworkRepository contactNetworkRepository)
        {
            _contactNetworkRepository = contactNetworkRepository;
        }

        // GET
        [HttpGet("Generate")]
        public async Task<IActionResult> Generate()
        {
            try
            {
                await _contactNetworkRepository.Clear();
                await _contactNetworkRepository.Generate();
                return Ok(true);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, "Error loading");
            }
        }

        // GET
        [HttpGet("Count")]
        public IActionResult GetCount()
        {
            try
            {
                var m = _contactNetworkRepository.GetAllCount();
                return Ok(m);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, "Error loading");
            }
        }

        // GET
        public IActionResult Get()
        {
            try
            {
                var m = _contactNetworkRepository.GetAll().ToList();
                return Ok(m);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, "Error loading");
            }
        }

        // GET
        [HttpGet("Tree")]
        public IActionResult GetTree()
        {
            try
            {
                var m = _contactNetworkRepository.GetAll().ToList();
                var tree=Mapper.Map<List<ContactTreeInfo>>(m);
                return Ok(tree);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, "Error loading");
            }
        }
    }
}
