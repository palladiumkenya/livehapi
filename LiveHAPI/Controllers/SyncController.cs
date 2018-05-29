using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces;
using LiveHAPI.Core.Model.Setting;
using LiveHAPI.Shared.Interfaces;
using LiveHAPI.Shared.ValueObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveHAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Sync")]
    public class SyncController : Controller
    {
        private readonly IRestManager _manager;
        private readonly IWritableOptions<Endpoints> _options;
        private readonly IWritableOptions<ConnectionStrings> _optionsb;

        public SyncController(IRestManager manager, IWritableOptions<Endpoints> options, IWritableOptions<ConnectionStrings> optionsb)
        {
            _manager = manager;
            _options = options;
            _optionsb = optionsb;
        }

        // GET: api/sync
        [HttpGet("hapi")]
        public virtual IActionResult ReadSetting()
        {

            try
            {
                var h = new HapiSettingsView();

                h.Connection = _optionsb.Value.HapiConnection;
                h.Url = _options.Value.Iqcare;



                return Ok(h);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Database setting";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }


        // GET: api/sync
        [HttpGet]
        public virtual IActionResult ReadUrl()
        {

            try
            {
                var endpoints = _options.Value;

                if (null == endpoints)
                    return NotFound();

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

        [HttpPost]
        public async Task<IActionResult> VerifyUrl([FromBody] Endpoints endpoints)
        {
            if (null==endpoints)
                return BadRequest();
            try
            {
                var practice =await  _manager.VerfiyUrl(endpoints);
                if (null == practice)
                    return NotFound();

                return Ok(practice);
            }
            catch (Exception e)
            {
                //Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
        [HttpPost("url")]
        public IActionResult SaveUrl([FromBody] Endpoints endpoints)
        {
            if (null==endpoints)
                return BadRequest();
            try
            {
                _options.Update(opt =>
                {
                    opt.Iqcare =endpoints.Iqcare;
                });

                return Ok(true);
            }
            catch (Exception e)
            {
                //Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
