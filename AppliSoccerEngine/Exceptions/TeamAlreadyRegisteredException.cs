using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliSoccerEngine.Exceptions
{
    public class TeamAlreadyRegisteredException : Exception
    {
        public TeamAlreadyRegisteredException()
            :base("Team already registered!")
        {

        }
    }
}
