using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class OrderReceiving
    {
        public String ReceiverId { get; set; }
        public Order Order { get; set; }
        public bool WasRead { get; set; }
    }
}
