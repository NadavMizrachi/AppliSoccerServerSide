using AppliSoccerEngine.Events;
using AppliSoccerObjects.Modeling;
using AppliSoccerObjects.ActionResults.EventsActions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliSoccerRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EventsController : Controller
    {
        private readonly ILogger<EventsController> _logger;
        private readonly EventsManager _eventsManager;
        public EventsController(ILogger<EventsController> logger)
        {
            _logger = logger;
            _eventsManager = new EventsManager();
        }

        [HttpPut]
        public async Task<CreateEventActionResult> CreateEvent(EventDetails eventDetails)
        {
            _logger.LogInformation("Got request for CreateEvent");
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Event details : {JsonConvert.SerializeObject(eventDetails)}");
            }
            try
            {
                CreateEventActionResult actionResult = await _eventsManager.CreateEvent(eventDetails);
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug($" CreateEventActionResult  = {JsonConvert.SerializeObject(actionResult)}");
                }
                return actionResult;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateEvent has fail.");
            }
            return null;
        }

        [HttpGet]
        public async Task<GetEventsActionResult> GetEvents(DateTime lowerBoundDate, DateTime upBoundDate, string askerId)
        {
            _logger.LogInformation("Got request for GetEvents");
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Request details - lowerBoundDate = {lowerBoundDate} " +
                    $"upBOundDate = {upBoundDate}  askerId = {askerId}");
            }
            try
            {
                GetEventsActionResult actionRes = await _eventsManager.GetEvents(lowerBoundDate, upBoundDate, askerId);
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug($" GetEventsActionResult  = {JsonConvert.SerializeObject(actionRes)}");
                }
                return actionRes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetEvent has failed");
            }
            return null;
        }

        [HttpPost]
        public async Task<EditEventActionResult> EditEvent(EventDetails edittedEvent)
        {
            _logger.LogInformation("Got request for editEvent");
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"New event details: {JsonConvert.SerializeObject(edittedEvent)}");
            }
            try
            {
                EditEventActionResult actionRes = await _eventsManager.editEvent(edittedEvent);
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug($"EditEventActionResult = {JsonConvert.SerializeObject(actionRes)}");
                }
                return actionRes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Edit event has failed");
            }
            return null;
        }



    }
}
