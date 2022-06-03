using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine.Exceptions
{
    public class CoachAlreadyExistsException : Exception
    {
        public CoachAlreadyExistsException()
        {
        }

        public CoachAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
