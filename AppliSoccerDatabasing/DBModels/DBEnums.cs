using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class DBEnums
    {
        public enum Role { GoalKeeper, Defender, Midfielder, Attacker }

        public enum EventType { Game, Training, Volunteering, Forging, Medicine, Other }

        public enum MemberType { Admin, Staff, Player }
    }
}
