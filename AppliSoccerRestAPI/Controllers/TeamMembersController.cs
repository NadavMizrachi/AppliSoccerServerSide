using AppliSoccerEngine.TeamMembers;
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
    public class TeamMembersController : Controller
    {
        private readonly ILogger<TeamMembersController> _logger;
        private readonly TeamMembersManager _teamMembersManager;
        public TeamMembersController(ILogger<TeamMembersController> logger)
        {
            _logger = logger;
            _teamMembersManager = new TeamMembersManager();
        }

        [HttpGet]
        public async Task<List<TeamMember>> GetMembers([FromQuery] string teamId)
        {
            _logger.LogInformation("Got request of GetMembers for team id : " + teamId);
            var teamMembers = await _teamMembersManager.GetTeamMembers(teamId);
            return teamMembers;
        }


        //[HttpPost]
        //public async Task<bool> UpdateMember(TeamMember teamMember)
        //{

        //}
    }
}
