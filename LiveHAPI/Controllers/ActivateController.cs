﻿using System;
using LiveHAPI.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    
    [Route("api/activate")]
    public class ActivateController : Controller
    {
        private readonly IActivationService _activationService;
        private readonly ILogger<ActivateController> _logger;

        public ActivateController(ILogger<ActivateController> logger, IActivationService activationService)
        {
            _logger = logger;
            _activationService = activationService;
        }
        [Route("central")]
        [HttpGet]
        public IActionResult FindCentral()
        {
            try
            {
                var fac = _activationService.GetCentral();
                return Ok(fac);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("local")]
        [HttpGet]
        public IActionResult FindLocal()
        {
            try
            {
                var fac = _activationService.GetLocal();
                return Ok(fac);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("find/{code}")]
        [HttpGet]
        public IActionResult FindFacility(int code)
        {
            try
            {
                var fac = _activationService.Verify(code);
                return Ok(fac);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"Error loading counties: {e}");
                return StatusCode(500, "Error loading counties");
            }
        }
        [Route("enroll/{code}")]
        [HttpGet]
        public IActionResult EnrollSite(string code)
        {
            try
            {
                var practice = _activationService.EnrollPractice(code);
                return Ok(practice);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"Error loading Practice: {e}");
                return StatusCode(500, "Error loading Practice");
            }
        }
    }
}