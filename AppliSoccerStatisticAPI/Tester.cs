using AppliSoccerStatisticAPI.ExternalAPISource.API_Football;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerStatisticAPI
{
    public class Tester
    {
        public static void Main(string[] args)
        {
            APIServer server = new APIServer();
            Console.WriteLine("Hello world");
            //LeaguesInfo info = await server.GetLeaguesInfo("563", "2021");

            var table = server.GetLeagueTable("383", "2021").Result;



            Console.ReadLine();
        }
    }
}
