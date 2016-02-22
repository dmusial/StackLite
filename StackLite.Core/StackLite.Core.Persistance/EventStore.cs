using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Persistance
{
    public class EventStore : IEventStore
    {
        private struct EventDescriptor
        {
            public Guid EventId { get; set; }
            public string EventType { get; set; }
            public readonly Event Data;

            public EventDescriptor(Guid eventId, string eventType, Event data)
            {
                EventId = eventId;
                EventType = eventType;
                Data = data;
            }
        }
     
        private readonly string _embedEventContentSwitch = "?embed=body";
        private ILogger _logger;
     
        public EventStore(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EventStore>();       
        }
        
        public List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            using (var client = new HttpClient())
            {
                var streamName = BuildStreamName(aggregateId);
                client.BaseAddress = new Uri("http://localhost:2113/streams/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.eventstore.atom+json"));
                var response = client.GetAsync(streamName + _embedEventContentSwitch).Result;
                
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var parsedResponse = JObject.Parse(responseContent);
                var eventsData = parsedResponse["entries"];
                
                Event[] events = new Event[eventsData.Count()];
                foreach (var eventEntry in eventsData)
                {
                    var eventTypeName = (string)eventEntry["eventType"];
                    var eventData = JsonConvert.SerializeObject(eventEntry["data"]);
                    
                    var @event = EventDeserializer.Deserialize(eventTypeName, eventData);
                    
                    int eventNumber = int.Parse((string)eventEntry["eventNumber"]);
                    events[eventNumber] = @event;
                }
                
                return events.ToList();
            }
        }

        private string ClearEventData(string eventData)
        {
            return eventData.Trim('\"').Replace("\\r\\n", "").Replace("\\", "");
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                string streamName = BuildStreamName(aggregateId);
                SaveEvent(@event, streamName);
            }
        }

        private async void SaveEvent(Event @event, string streamName)
        {
            var eventDescriptor = new EventDescriptor(Guid.NewGuid(), @event.GetType().Name, @event);
            var serializedEvent = JsonConvert.SerializeObject(new EventDescriptor[] { eventDescriptor });
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:2113/streams/");
                var response = await client.PostAsync(streamName, new StringContent(serializedEvent, Encoding.UTF8, "application/vnd.eventstore.events+json"));
                
                _logger.LogInformation(string.Format("Saving event {0} to stream resulted with {1}", eventDescriptor.EventType, response.StatusCode));
            }
        }
        
        private string BuildStreamName(Guid aggregateId)
        {
            return aggregateId.ToString();
        }
    }
}