using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class OrderReceivingDBModel
    {
        public String ReceiverId { get; set; }
        public OrderDBModel Order { get; set; }
        public bool WasRead { get; set; }

    }
}
