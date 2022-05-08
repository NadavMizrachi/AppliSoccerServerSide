using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerStatisticAPI.ExternalAPISource
{
    public interface IStatisticAPI
    {
        public Task<IEnumerable<Team>> GetTeamsTask(IEnumerable<string> countryNames);
    }
}
