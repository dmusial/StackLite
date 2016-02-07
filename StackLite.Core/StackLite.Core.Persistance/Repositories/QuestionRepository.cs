using System;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Questions;

namespace StackLite.Core.Persistance
{    
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ILogger _logger;
        private readonly IEventStore _eventStore;
    
        public QuestionRepository(IEventStore eventStore, ILoggerFactory loggerFactory)
        {
            _eventStore = eventStore;
            _logger = loggerFactory.CreateLogger<QuestionRepository>();       
        }

        public Question Get(Guid questionId)
        {
            var events = _eventStore.GetEventsForAggregate(questionId);
            return new Question(events);
        }
        
        public void Save(Question question)
        {
            _eventStore.SaveEvents(question.Id, question.GetUncommittedEvents());
        }
    }
}
