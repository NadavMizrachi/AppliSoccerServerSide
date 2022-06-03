using AppliSoccerEngine.TeamMembers;
using AppliSoccerObjects.Modeling;
using AppliSoccerRestAPI.MyCustomBinders;
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
            try
            {
                _logger.LogInformation("Got request of GetMembers for team id : " + teamId);
                var teamMembers = await _teamMembersManager.GetTeamMembers(teamId);
                _logger.LogInformation($"GetMembers for teamID {teamId} has succeed!");
                return teamMembers;
            }
            catch(Exception ex)
            {
                _logger.LogError($"GetMembers for teamID {teamId} has failed!", ex);
                return null;
            }
        }

        [HttpPost]
        public async Task<TeamMember> UpdateTeamMember([ModelBinder(typeof(TeamMemberBinder))] TeamMember memberNewDetails)
        {
            try
            {
                _logger.LogInformation($"Got request of UpdateTeamMember for member: ${memberNewDetails.ID}");
                TeamMember updatedMember = await _teamMembersManager.UpdateMemberDetails(memberNewDetails);
                _logger.LogInformation($"UpdateTeamMember for member: ${memberNewDetails.ID} has succeed!");
                return updatedMember;
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateTeamMember for member: ${memberNewDetails.ID} has failed!", ex);
                return null;
            }
        }

        [HttpDelete]
        public async Task<bool> RemoveMember([ModelBinder(typeof(TeamMemberBinder))] TeamMember memberToRemove)
        {
            try
            {
                _logger.LogInformation($"Got request to RemoveMember : ${memberToRemove.ID}");
                bool isRemoved = await _teamMembersManager.RemoveMember(memberToRemove);
                _logger.LogInformation($"RemoveMember for member success? :  ${isRemoved} has succeed!");
                return isRemoved;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Removing for member: ${memberToRemove.ID} has failed!", ex);
                return false;
            }
        }

        //[HttpPost]
        //public async Task<bool> UpdateMember(TeamMember teamMember)
        //{

        //}
    }
}
