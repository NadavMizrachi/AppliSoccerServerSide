﻿using System;
using System.Collections.Generic;
using System.Text;
using static AppliSoccerDatabasing.DBModels.DBEnums;

namespace AppliSoccerDatabasing.DBModels
{
    public class PlayerAdditionalInfoDBModel : AdditionalInfoDBModel
    {
        public int Number { get; set; }
        public Role Role { get; set; }
    }
}
