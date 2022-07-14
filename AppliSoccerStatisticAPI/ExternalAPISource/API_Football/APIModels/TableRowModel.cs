using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels
{
    public class TableRowModel
    {
        public int Rank { get; set; }
        public string TeamName{ get; set; }
        public string TeamExtId { get; set; }
        public int Points { get; set; }
        public int GoalsDiff { get; set; }
        public string Form { get; set; }
    }
}
