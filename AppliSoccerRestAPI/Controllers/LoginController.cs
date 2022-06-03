using AppliSoccerEngine.Login;
using AppliSoccerObjects.Modeling;
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
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly LoginManager _loginManager;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            _loginManager = new LoginManager();
        }

        [HttpGet]
        public async Task<TeamMember> Login(string username, string password)
        {
            _logger.LogInformation($"Login attempt. For username: ${username}");
            try
            {
                var teamMember = await _loginManager.Login(username, password);
                return teamMember;
            }catch(Exception ex)
            {
                _logger.LogError($"Error has occured during login of user: ${username}", ex);
            }
            return null;
        }
    }
}
