using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Answers;

namespace StackLite.Core.Persistance
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ILogger _logger;
        private readonly IEventStore _eventStore;
        
        public AnswerRepository(IEventStore eventStore, ILogger logger)
        {
            _eventStore = eventStore;
            _logger = logger;
        }

        public Answer Get(Guid answerId)
        {
            var events = _eventStore.GetEventsForAggregate(answerId);
            return new Answer(events);
        }

        public List<Answer> GetAllFor(Guid questionId)
        {
            return new List<Answer>();    
        }

        public void Save(Answer answer)
        {
            _eventStore.SaveEvents(answer.Id, answer.GetUncommittedEvents());
        }
    }
}