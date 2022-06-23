using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class OrderMetadata
    {
        public String Title { get; set; }
        public String SenderName { get; set; }
        public DateTime SentDate { get; set; }
        public String OrderId { get; set; }
        public bool WasRead { get; set; }
        public List<Role> RolesReceivers { get; set; }
        public List<string> ReceiversNames { get; set; }

    }

}
