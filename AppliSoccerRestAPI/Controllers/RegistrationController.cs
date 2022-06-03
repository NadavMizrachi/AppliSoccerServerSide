using AppliSoccerEngine.Registration;
using AppliSoccerObjects.Modeling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppliSoccerEngine.Exceptions;
using AppliSoccerObjects.ResponseObjects;
using AppliSoccerRestAPI.MyCustomBinders;

namespace AppliSoccerRestAPI.Controllers
{
    // TODO Add time tag to logger
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
            _logger.LogInformation("Got request of GetUnregisteredTeams");
            var teams = await _registrationManager.GetUnregisteredTeams(country);
            return teams;
        }

        [HttpGet]
        public List<string> GetCountries()
        {
            _logger.LogInformation("Got request of GetCountries");
            return _registrationManager.GetCountries().ToList();
        }

        [HttpGet]
        public string test([FromQuery]string country)
        {
            return country;
        }

        [HttpPost]
        public TeamMember RegisterTeam([FromQuery] string teamId,
            [FromQuery] string username,
            [FromQuery] string password)
        {
            _logger.LogInformation($"Got request to RegisterTeam. Team_ID: ${teamId}    Username: ${username}   Password: ${password}");
            TeamMember teamMember = null;
            try
            {
                teamMember = _registrationManager.RegisterTeam(teamId, username, password);
            }catch (UsernameAlreadyExistsException ex)
            {
                _logger.LogInformation($"User name ${username} is already registered.");
                return null;
            }
            catch (TeamAlreadyRegisteredException ex)
            {
                _logger.LogInformation($"Team with teamId: ${teamId} is already registered.");
                return null;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error whule trying to register temaid: ${teamId}");
                return null;
            }
            return teamMember;
        }

        [HttpPut]
        public async Task<bool> CreateUser([ModelBinder(typeof(UserBinder))]User user)
        {
            _logger.LogInformation("Got request of CreateUser. Details: Username " + user.Username + " TeamMember: " + user.TeamMember);
            var isCreationSucceed = false;
            try
            {
                await _registrationManager.RegisterUser(user);
                isCreationSucceed = true;
            }catch(UsernameAlreadyExistsException ex)
            {
                _logger.LogInformation($"User name ${user.Username} is already registered.");
            }
            catch ( Exception ex)
            {
                _logger.LogError("Error has occurred while trying to create new user : " + user.Username, ex.Message);
            }
            return isCreationSucceed;
        }
    }
      
}
