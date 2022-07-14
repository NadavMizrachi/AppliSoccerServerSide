using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.ActionResults.EventsActions;
using AppliSoccerObjects.Modeling;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing.mongo
{
    public class EventQueries
    {
        private IMongoCollection<EventDetailsDBModel> _collection;

        public EventQueries(IMongoCollection<EventDetailsDBModel> eventsCollection)
        {
            _collection = eventsCollection;
        }

        public Task InsertEvent(EventDetails eventDetails)
        {
            // Convert to DB model
            EventDetailsDBModel eventModel = DBModelConverter.ConvertEvent(eventDetails);
            return _collection.InsertOneAsync(eventModel);
        }

        public Task<bool> isExistEventForParticipantsBetweenDates(DateTime start, DateTime end, List<string> participantsIds)
        {
            return Task.Run( () => {
                List<EventDetailsDBModel> eventModels =
                    _collection
                        .Find(eventModel => eventModel.ParticipantsIds.Any(id => participantsIds.Contains(id)))
                        .ToList();

                return eventModels.Any(model => 
                    {
                        return (model.StartTime.Ticks >= start.Ticks && model.StartTime.Ticks <= end.Ticks) ||
                                (model.EndTime.Ticks >= start.Ticks && model.EndTime.Ticks <= end.Ticks);
                    }
                    );
            });
        }

        public async Task<List<EventDetails>> GetEvents(DateTime lowerBoundDate, DateTime upBoundDate, string askerId)
        {
            List<EventDetailsDBModel> eventModels =
                (await _collection.FindAsync(eventModel => eventModel.ParticipantsIds.Contains(askerId))).ToList();
            List<EventDetailsDBModel> eventModelsInRange =
                eventModels
                .Where(eventModel =>
                    {
                        return eventModel.StartTime.Ticks >= lowerBoundDate.Ticks &&
                                eventModel.EndTime.Ticks <= upBoundDate.Ticks;
                    }
                )
                .OrderBy(e => e.StartTime)
                .ToList();

            return DBModelConverter.ConvertEvents(eventModelsInRange);
        }

        public Task UpdateEvent(EventDetails edittedEvent)
        {
            return Task.Run( () =>
            {
                var idToEdit = edittedEvent.Id;
                EventDetailsDBModel eventWithNewValues = DBModelConverter.ConvertEvent(edittedEvent);

                EventDetailsDBModel eventModelToEdit =
                    _collection.Find(eventModel => eventModel.Id.Equals(idToEdit)).First();

                eventModelToEdit.UpdateWithNewValues(eventWithNewValues);

                var filter = Builders<EventDetailsDBModel>.Filter.Eq(eventModel => eventModel.Id, idToEdit);
                _collection.ReplaceOne(filter, eventModelToEdit);
            });

        }
    }
}