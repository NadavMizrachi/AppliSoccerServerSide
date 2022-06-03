using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine.Exceptions
{
    class UserWithUnvalidDataException : Exception
    {
        public UserWithUnvalidDataException()
        {
        }

        public UserWithUnvalidDataException(string message) : base(message)
        {
        }
    }
}
