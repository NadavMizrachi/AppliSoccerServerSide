using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class SentOrderWithReceiversInfo
    {
        public Order Order { get; set; }
        public List<ReceiverInfo> ReceiverInfos { get; set; }
    }
    public class ReceiverInfo
    {
        public string Name { get; set; }
        public bool Read { get; set; }
    }
}
