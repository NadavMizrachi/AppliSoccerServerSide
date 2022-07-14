using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class TableRowDBModel
    {
        public string TeamName { get; set; }
        public string TeamId { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int GoalsDiff { get; set; }
        public string Form { get; set; }
        public string TeamLogoUrl { get; set; }
    }
}
