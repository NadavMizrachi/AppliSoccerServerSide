using AppliSoccerStatisticAPI.RestEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football
{
    public class APIFootbalServerConfiguration : ServerConfiguration
    {
        private string _protocol;
        private string _hostURL;
        private List<RequestParameter> _requestParameters;
        public override string Protocol { get => _protocol; }
        public override string HostURL { get => _hostURL; }
        public override List<RequestParameter> HeaderParameters { get => _requestParameters; }

        public APIFootbalServerConfiguration()
        {
            _protocol = "https://";
            _hostURL = "api-football-v1.p.rapidapi.com/v3/";
            _requestParameters = new List<RequestParameter>();
            _requestParameters.Add(new RequestParameter() { Key = "X-RapidAPI-Host", Value = "api-football-v1.p.rapidapi.com" });
            _requestParameters.Add(new RequestParameter() { Key = "X-RapidAPI-Key", Value = "2da78f1a5amsh596f18457aea569p16f57djsndc64b15c76a0" });
        }
    }
}
