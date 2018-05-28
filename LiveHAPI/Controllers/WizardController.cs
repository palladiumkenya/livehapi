using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces;
using LiveHAPI.Core.Model.Setting;
using LiveHAPI.Shared.Interfaces;
using LiveHAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveHAPI.Controllers
{
    [Route("api/wizard")]
    public class WizardController : Controller
    {
        private IDbManager _dbManager;
        private readonly IWritableOptions<ConnectionStrings> _options;

        public WizardController(IDbManager dbManager, IWritableOptions<ConnectionStrings> options)
        {
            _dbManager = dbManager;
            _options = options;
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


        [HttpPost("verifydb")]
        public IActionResult VerifyDb([FromBody] DbProtocol dbProtocol)
        {
            if (null == dbProtocol)
                return BadRequest();

            try
            {
                var connected= _dbManager.Verfiy(dbProtocol);

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
        public IActionResult SaveConnection([FromBody] DbView dbView)
        {
            if (null == dbView)
                return BadRequest();
            try
            {
                _options.Update(opt =>
                {
                    opt.HapiConnection = _dbManager.BuildConncetion(dbView.Hapi);
                    opt.EmrConnection = _dbManager.BuildConncetion(dbView.Emr);
                });
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
