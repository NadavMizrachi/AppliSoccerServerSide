using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliSoccerRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DebugController : Controller
    {
        [HttpGet]
        public string Ping()
        {
            return "Pong";
        }
    }
}
