﻿using System;
using System.Collections.Generic;
using System.Text;


namespace AppliSoccerObjects.Modeling
{
    public class Order
    {
        public String ID { get; set; }
        public String Title { get; set; }
        public DateTime SendingDate { get; set; }
        public String SenderId { get; set; }
        public String TeamId { get; set; }
        public List<Role> RolesReceivers { get; set; }
        public List<String> MemberIdsReceivers { get; set; }
        public String Content { get; set; }
        public List<String> MembersIdsAlreadyRead { get; set; }
        public String GameId { get; set; }

    }
}
