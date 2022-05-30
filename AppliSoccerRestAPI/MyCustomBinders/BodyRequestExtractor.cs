using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppliSoccerRestAPI.MyCustomBinders
{
    public class BodyRequestExtractor
    {
        public static Task<string> ExtractAsJson(HttpRequest request)
        {
            var requestBody = request.Body;
            using (var reader = new StreamReader(requestBody, System.Text.Encoding.UTF8))
            {
                return reader.ReadToEndAsync();
            }
        }
    }
}
