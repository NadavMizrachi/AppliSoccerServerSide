using System;
using System.Collections.Generic;
using System.Text;
using static AppliSoccerDatabasing.DBModels.DBEnums;

namespace AppliSoccerDatabasing.DBModels
{
    public class PlayerDBModel : TeamMemberDBModel
    {
        public int Number { get; set; }
        public Role Role { get; set; }
    }
}
