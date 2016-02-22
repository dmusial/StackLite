using System;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Answers;

namespace StackLite.Core.Persistance
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ILogger _logger;
        private readonly IEventStore _eventStore;
        
        public AnswerRepository(IEventStore eventStore, ILoggerFactory loggerFactory)
        {
            _eventStore = eventStore;
            _logger = loggerFactory.CreateLogger<AnswerRepository>();
        }

        public Answer Get(Guid answerId)
        {
            var events = _eventStore.GetEventsForAggregate(answerId);
            return new Answer(events);
        }

        public void Save(Answer answer)
        {
            _eventStore.SaveEvents(answer.Id, answer.GetUncommittedEvents());
        }
    }
}