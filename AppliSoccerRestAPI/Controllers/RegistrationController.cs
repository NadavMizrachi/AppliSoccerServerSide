using AppliSoccerEngine.Registration;
using AppliSoccerObjects.Modeling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliSoccerRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private RegistrationManager _registrationManager;
        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
            _registrationManager = new RegistrationManager();
        }


        [HttpGet]
        public async Task<List<TeamDetails>> GetUnregisteredTeams([FromQuery]string country)
        {
            var teams = await _registrationManager.GetUnregisteredTeams(country);
            return teams;
        }

        [HttpGet]
        public List<string> GetCountries()
        {
            return _registrationManager.GetCountries().ToList();
        }

        [HttpGet]
        public string test([FromQuery]string country)
        {
            return country;
        }

        [HttpPost]
        public bool RegisterTeam([FromQuery] string teamId,
            [FromQuery] string username,
            [FromQuery] string password)
        {

        }
    }
      
}
