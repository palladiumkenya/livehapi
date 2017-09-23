using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    
    [Route("api/forms")]
    public class FormsController : Controller
    {
        private readonly IFormsService _formsService;
        public FormsController( IFormsService formsService)
        {
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
                Log.Debug($"{e}");
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
                Log.Debug($"{e}");
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
                Log.Debug($"{e}");
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
                Log.Debug($"{e}");
                return StatusCode(500, "Error loading forms");
            }
        }
    }
}
