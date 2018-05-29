using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Setting;
using LiveHAPI.Custom;
using LiveHAPI.Shared.Interfaces;
using LiveHAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveHAPI.Controllers
{
    [Route("api/wizard")]
    public class WizardController : Controller
    {
        private readonly IWizardService _wizardService;
        private IDbManager _dbManager;
        private readonly IWritableOptions<ConnectionStrings> _options;

        public WizardController(IDbManager dbManager, IWritableOptions<ConnectionStrings> options)
        {
            _dbManager = dbManager;
            _options = options;
        }


        // GET: api/wizard/db
        [HttpGet("db")]
        public virtual IActionResult GetDb()
        {
            try
            {
                var dbProtocol = _dbManager.ReadConnection(_options.Value.HapiConnection);

                if (null == dbProtocol)
                    return NotFound();

                return Ok(dbProtocol);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Database setting";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpPost("verifydb")]
        public IActionResult VerifyDb([FromBody] DbProtocol dbProtocol)
        {
            if (null == dbProtocol)
                return BadRequest();

            try
            {
                var connected = _dbManager.Verfiy(dbProtocol);

                if (connected)
                    return Ok(true);

                throw new Exception("Database could not connect");
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpPost("db")]
        public IActionResult SaveConnection([FromBody] DbProtocol dbProtocol)
        {
            if (null == dbProtocol)
                return BadRequest();
            try
            {
                _options.Update(opt =>
                {
                    opt.HapiConnection = _dbManager.BuildConncetion(dbProtocol);
                });

                //Startup.ServiceCollection.ConfigureWritable<ConnectionStrings>(Startup.Configuration.GetSection("connectionStrings"));
                return Ok(true);
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        // GET: api/wizard/url
        [HttpGet("url")]
        public virtual IActionResult GetUrl()
        {
            try
            {
                Endpoints endpoints = null;

                return Ok(endpoints);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Database setting";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpPost("verifyurl")]
        public IActionResult VerifyUrl([FromBody] Endpoints endpoints)
        {
            if (null == endpoints)
                return BadRequest();

            try
            {

                throw new Exception("Url cant connect");
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }


        [HttpPost("url")]
        public IActionResult SaveUrl([FromBody] Endpoints endpoints)
        {
            if (null == endpoints)
                return BadRequest();

            try
            {

                throw new Exception("Url could not be saved");
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
