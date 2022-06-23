using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class OrderPayload
    {
        public string SenderName { get; set; }
        public List<string> ReceiversNames { get; set; }
        public DateTime SendingDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string GameID { get; set; }
    }
}
