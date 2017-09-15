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
    
    [Route("api/forms")]
    public class FormsController : Controller
    {
        private readonly IFormsService _formsService;
        private readonly ILogger<FormsController> _logger;

        public FormsController(ILogger<FormsController> logger, IFormsService formsService)
        {
            _logger = logger;
            _formsService = formsService;
        }

        [Route("modules")]
        [HttpGet]
        public IActionResult GetData()
        {
            try
            {
                var m = _formsService.ReadModules().ToList();
                return Ok(m);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading forms");
            }
        }

        [Route("forms")]
        [HttpGet]
        public IActionResult GetForms()
        {
            try
            {
                var m = _formsService.ReadForms().ToList();
                return Ok(m);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading forms");
            }
        }

        [Route("concepts")]
        [HttpGet]
        public IActionResult GetConcepts()
        {
            try
            {
                var m = _formsService.ReadConcepts().ToList();
                return Ok(m);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading forms");
            }
        }

        [Route("questions")]
        [HttpGet]
        public IActionResult GetQuestions()
        {
            try
            {
                var m = _formsService.ReadQuestions().ToList();
                return Ok(m);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"{e}");
                return StatusCode(500, "Error loading forms");
            }
        }
    }
}
