using AppliSoccerEngine.Leagues;
using AppliSoccerObjects.ActionResults.LeagueActions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliSoccerRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LeaguesController : Controller
    {
        private readonly ILogger<LeaguesController> _logger;
        private readonly LeaguesManager _leaguesManager;
        public LeaguesController(ILogger<LeaguesController> logger)
        {
            _logger = logger;
            _leaguesManager = new LeaguesManager();
        }

        [HttpGet]
        public async Task<GetMainLeagueActionResult> GetMainLeague(string teamId)
        {
            _logger.LogInformation($"Got request of GetMainLeague for teamId : {teamId}");
            try
            {
                GetMainLeagueActionResult actionResult = await _leaguesManager.GetMainLeague(teamId);
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug($"GetMainLeagueActionResult = {JsonConvert.SerializeObject(actionResult)}");
                }
                return actionResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Got main league has failed");
                return null;
            }
        }
    }
}
