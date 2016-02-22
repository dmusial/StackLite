using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Persistance
{
    public static class EventDeserializer
    {
        public static Event Deserialize(string eventTypeName, string eventData)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Event));
            Type eventType = assembly.GetTypes().FirstOrDefault(t => t.FullName.EndsWith("." + eventTypeName));
            var eventDataJson = ClearEventData(eventData);

            return (Event)JsonConvert.DeserializeObject(eventDataJson, eventType);
        }
        
        private static string ClearEventData(string eventData)
        {
            return eventData.Trim('\"').Replace("\\r\\n", "").Replace("\\", "");
        }
    }
}