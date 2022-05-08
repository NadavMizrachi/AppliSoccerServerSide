using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class GamePreparation
    {
        public String GeneralOrder { get; set; }
        public GameEvent Game { get; set; }
        public List<Order> Orders { get; set; }
    }
}
