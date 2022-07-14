using AppliSoccerObjects.Modeling;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerStatisticAPI.ExternalAPISource
{
    public interface IStatisticAPI
    {
        Task<IEnumerable<Team>> GetTeamsTask(IEnumerable<string> countryNames);
        Task<LeaguesInfo> GetLeaguesInfo(string teamId, string season);
        Task<LeagueTableModel> GetLeagueTable(string leagueId, string season);

    }
}
