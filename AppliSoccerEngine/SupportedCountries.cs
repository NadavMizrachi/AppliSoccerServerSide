using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine
{
    public class SupportedCountries
    {
        private static IEnumerable<String> _countries = new String[] { "Israel", "England", "Spain" };

        public static IEnumerable<string> GetCountries() { return _countries; }
    }
}
