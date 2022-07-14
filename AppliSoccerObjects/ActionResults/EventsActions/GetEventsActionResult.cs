using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.ActionResults.EventsActions
{
    public enum GetEventFailReason { Unknown }
    public class GetEventsActionResult : ActionResult
    {
        public GetEventFailReason FailReason { get; set; }
        public List<EventDetails> Events { get; set; }
    }
}
