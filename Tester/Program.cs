using AppliSoccerStatisticAPI.ExternalAPISource.API_Football;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels;
using System;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            APIServer server = new APIServer();
            Console.WriteLine("Hello world");
            //LeaguesInfo info = await server.GetLeaguesInfo("563", "2021");

            var table = server.GetLeagueTable("383", "2021").Result;

            Console.ReadLine();
        }
    }
}
