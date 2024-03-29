﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class TeamMember
    {
        public string TeamId { get; set; }
        public String TeamName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public String ID { get; set; }
        public String Description { get; set; }
        public String PhoneNumber { get; set; }
        public MemberType MemberType{ get; set; }
        public Object AdditionalInfo { get; set; }

    }
}
