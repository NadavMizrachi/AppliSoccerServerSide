using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.ActionResults.EventsActions
{
    public enum EventFailReason { Unknown, EventExistsOnThisTime, EventDataIsUnvalid }
    public class CreateEventActionResult : ActionResult
    {
        public EventFailReason FailReason { get; set; }
    }
}
