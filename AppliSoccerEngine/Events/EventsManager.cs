using AppliSoccerDatabasing;
using AppliSoccerObjects.Modeling;
using AppliSoccerObjects.ActionResults;
using AppliSoccerObjects.ActionResults.EventsActions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AppliSoccerEngine.Events
{
    public class EventsManager
    {
        private static readonly log4net.ILog _logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDataBaseAPI _dataBaseAPI = Database.GetDatabase();
        private EventDetailsValidator _eventValidator = new EventDetailsValidator();
        public async Task<CreateEventActionResult> CreateEvent(EventDetails eventDetails)
        {
            CreateEventActionResult actionResult = new CreateEventActionResult();
            await assignAllTeamMembersToEvent(eventDetails);
            // Validate details 
            if(!_eventValidator.IsValid(eventDetails))
            {
                actionResult.Status= Status.Fail;
                actionResult.FailReason = EventFailReason.EventDataIsUnvalid;
            }
            else if(await _dataBaseAPI.IsExistOverlappingEvent(eventDetails))
            {
                actionResult.Status = Status.Fail;
                actionResult.FailReason = EventFailReason.EventExistsOnThisTime;
            }
            else
            {
                await _dataBaseAPI.InsertEvent(eventDetails);
                actionResult.Status = Status.Success;
            }
            return actionResult;
        }

        private async Task assignAllTeamMembersToEvent(EventDetails eventDetails)
        {
            List<TeamMember> teamMembers = await _dataBaseAPI.GetTeamMembers(eventDetails.TeamId);
            eventDetails.ParticipantsIds = teamMembers.Select(tm => tm.ID).ToList();
            eventDetails.ParticipantsRoles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
        }

        public async Task<GetEventsActionResult> GetEvents(DateTime lowerBoundDate, DateTime upBoundDate, string askerId)
        {
            GetEventsActionResult actionResult = new GetEventsActionResult();
            try
            {
                _logger.Info("Asking database for events in range");
                List<EventDetails> events = await _dataBaseAPI.GetEvents(lowerBoundDate, upBoundDate, askerId);
                    
                actionResult.Status = Status.Success;
                actionResult.Events = events;
            }
            catch(Exception ex)
            {
                _logger.Error("Error has occurred durring trying to get events from database", ex);
                actionResult.Status = Status.Fail;
            }
            return actionResult;
        }

        public async Task<EditEventActionResult> editEvent(EventDetails edittedEvent)
        {
            EditEventActionResult actionResult = new EditEventActionResult();
            try
            {
                _logger.Info("Propagating edit event to database");
                await assignAllTeamMembersToEvent(edittedEvent);
                await _dataBaseAPI.UpdateEvent(edittedEvent);
                actionResult.Status = Status.Success;
            }
            catch (Exception ex)
            {
                _logger.Error("Error has occurred while trying to update event details on database.", ex);
                actionResult.Status = Status.Fail;
            }
            return actionResult;
        }
    }
}
