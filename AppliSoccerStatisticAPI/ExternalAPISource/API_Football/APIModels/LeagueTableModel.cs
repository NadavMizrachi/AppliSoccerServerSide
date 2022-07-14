using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels
{
    public class LeagueTableModel
    {
        public string LeagueExtId { get; set; }
        public string LeagueName { get; set; }
        public string Country { get; set; }
        public string LeagueLogoURL { get; set; }
        public string LeagueFlagURL { get; set; }
        public List<SubLeague> SubLeagues { get; set; }
    }

    public class SubLeague
    {
        public string Description { get; set; }
        public List<TableRowModel> Rows { get; set; }
    }
}
