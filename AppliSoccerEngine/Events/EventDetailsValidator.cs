using AppliSoccerObjects.Modeling;
using System;

namespace AppliSoccerEngine.Events
{
    public class EventDetailsValidator
    {
        private static readonly log4net.ILog _logger =
               log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool IsValid(EventDetails eventDetails)
        {
            _logger.Info("Inside IsValid. Checks if event is valid");
            if (eventDetails == null) return false;
            if (eventDetails.CreatorId == null) return false;
            if (eventDetails.StartTime == null) return false;
            if (eventDetails.EndTime == null) return false;
            if (eventDetails.ParticipantsIds == null || eventDetails.ParticipantsIds.Count == 0) return false;
            if (eventDetails.Place == null) return false;

            return true;
        }
    }
}