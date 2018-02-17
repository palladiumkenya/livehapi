using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.Shared.Interfaces;
using LiveHAPI.Shared.ValueObject;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveHAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/setup")]
    public class SetupController : Controller
    {
        private readonly IPracticeRepository _configRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ISetupFacilty _setupFacilty;
        public SetupController(IPracticeRepository configRepository, IUserRepository userRepository, IPersonRepository personRepository, ISetupFacilty setupFacilty)
        {
            _configRepository = configRepository;
            _userRepository = userRepository;
            _personRepository = personRepository;
            _setupFacilty = setupFacilty;
        }

        [HttpGet("fac")]
        public IActionResult GetFacility()
        {
            try
            {
                var practice = _configRepository.GetDefault();
                return Ok(practice);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpGet("user")]
        public IActionResult GetUsers()
        {
            try
            {
                Log.Debug("loading users from emr");
                _setupFacilty.SyncUsers();
            }
            catch (Exception e)
            {
                Log.Error($"{e}");
            }

            try
            {
                var dtos=new List<UserDTO>();
                var users = _userRepository.GetAll().ToList();
                foreach (var user in users)
                {
                   var userdto=  Mapper.Map<UserDTO>(user);
                    var person = _personRepository.GetProvider(userdto.PersonId);
                    userdto.Person = Mapper.Map<PersonDTO>(person);
                    userdto.Provider = Mapper.Map<ProviderDTO>(person.Providers.FirstOrDefault());
                    dtos.Add(userdto);
                }
                return Ok(dtos);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
