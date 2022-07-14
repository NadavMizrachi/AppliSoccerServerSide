using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class SubTable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TableRow> Rows { get; set; }
    }
}
