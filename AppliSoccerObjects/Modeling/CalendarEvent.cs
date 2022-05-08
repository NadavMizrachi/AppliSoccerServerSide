using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class CalendarEvent
    {
        public EventDetails Details { get; set; }
        public virtual EventType Type { get; set; }
    }
}
