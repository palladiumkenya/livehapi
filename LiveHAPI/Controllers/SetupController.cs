using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
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
        private readonly IPracticeRepository _practiceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        public SetupController(IPracticeRepository practiceRepository, IUserRepository userRepository, IPersonRepository personRepository)
        {
            _practiceRepository = practiceRepository;
            _userRepository = userRepository;
            _personRepository = personRepository;
        }

        [HttpGet("fac")]
        public IActionResult GetFacility()
        {
            try
            {
                var practice = _practiceRepository.GetDefault();
                return Ok(practice);
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                return StatusCode(500, $"{e.Message}");
            }
        }

        [HttpGet("facall")]
        public IActionResult GetFacilities()
        {
            try
            {
                var practice = _practiceRepository.GetAllDefault();
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
                //TODO refresh Users
                Log.Debug("loading users from emr");
                //_setupFacilty.SyncUsers();
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
