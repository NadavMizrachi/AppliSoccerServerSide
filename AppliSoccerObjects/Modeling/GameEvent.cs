using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class GameEvent : CalendarEvent
    {
        public override EventType Type { get { return EventType.Game; } set { } }

        public GameScore Score { get; set; }
    }
}
