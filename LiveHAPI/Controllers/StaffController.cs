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
    
    [Route("api/staff")]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger, IStaffService staffService)
        {
            _logger = logger;
            _staffService = staffService;
        }

        [Route("persons")]
        [HttpGet]
        public IActionResult GetPersons()
        {
            try
            {
                var m = _staffService.ReadStaffInfo().ToList();
                return Ok(m);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading Staff information");
            }
        }

        [Route("users")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var m = _staffService.ReadUsers().ToList();
                return Ok(m);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading Staff information");
            }
        }

        [Route("providers")]
        [HttpGet]
        public IActionResult GetForms()
        {
            try
            {
                var m = _staffService.ReadProviders().ToList();
                return Ok(m);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading Staff information");
            }
        }

        
    }
}
