using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class SubTableDBModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TableRowDBModel> Rows { get; set; }
    }
}
