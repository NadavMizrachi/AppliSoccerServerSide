using System;
using AppliSoccerObjects.Modeling;
using AppliSoccerObjects.ResponseObjects;
using AppliSoccerStatisticAPI.RestEngine;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using AppliSoccerStatisticAPI.ExternalAPISource;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestDeSe();
            //TestAPI();
            TestAPINew();
        }

        private static void TestAPINew()
        {
            IStatisticAPI api = new APIServer();
            var countries = new List<string>();
            countries.Add("Israel");
            countries.Add("Spain");
            var teamsTask = api.GetTeamsTask(countries);
            var res = teamsTask.Result;
            foreach (var team in res)
            {
                Console.WriteLine(team);
            }
        }

        private static void TestAPI()
        {
            //RequestExecuter re = new RequestExecuter();
            //var res = re.ExecuteAsync().Result;
            //var content = res.Content;
            //RestResponseWrraper wrraper = JsonConvert.DeserializeObject<RestResponseWrraper>(content);
            //List<TeamInformations> teamInformations = JsonConvert.DeserializeObject<List<TeamInformations>>(wrraper.Response.ToString());
            //Console.WriteLine(teamInformations);
            //Console.WriteLine(res);
            //foreach (var teamInfo in teamInformations)
            //{
            //    Console.WriteLine(teamInfo);
            //}
            Console.ReadLine();
        }

        private static void TestDeSe()
        {
            Order order = new Order()
            {
                Content = "Order content",
                From = "Elyaniv",
                To = "Miguel",
                WasRead = false
            };

            RestObjectWrraper wrraper = RestObjectWrraperSerializer.Serialize(order);
            Console.WriteLine("wrraper.Data: = " + wrraper.Data + " wrraper.Type = " + wrraper.Type);
            Order des = RestObjectWrraperDeserializer<Order>.Deserialize(wrraper);
            Console.WriteLine($"Content :" + des.Content + " From :" + des.From + " To :" + des.To + " wasRead=" + des.WasRead);
        }
    }
}
