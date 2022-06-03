using System;
using System.Collections.Generic;
using System.Text;
using static AppliSoccerDatabasing.DBModels.DBEnums;

namespace AppliSoccerDatabasing.DBModels
{
    public class StaffAdditionalInfoDBModel : AdditionalInfoDBModel
    {
        public bool IsCoach { get; set; }
        public List<Role> ManagedRoles { get; set; }
    }
}
