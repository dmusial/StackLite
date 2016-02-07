using System.Linq;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Questions;
using StackLite.Core.FakeReportingStores;

namespace StackLite.Core.EventHandlers
{
    public interface IQuestionHandler
    {
        void Handle(QuestionAsked @event);
        void Handle(QuestionAmended @event);
    }
    
    public class QuestionHandler : IQuestionHandler, Handles<QuestionAsked>, Handles<QuestionAmended>
    {
        private readonly IQuestionsStore _questionsStore;
        private readonly ILogger _logger;
        
        public QuestionHandler(IQuestionsStore questionsStore, ILoggerFactory loggerFactory)
        {
            _questionsStore = questionsStore;
            _logger = loggerFactory.CreateLogger<QuestionHandler>();  
        }
        
        public void Handle(QuestionAmended @event)
        {
            _logger.LogInformation("Handler for QuestionAmended executed");
            var question = _questionsStore.Questions.FirstOrDefault(q => q.Id == @event.Id);
            if (question != null)
            {
                question.Content = @event.Content;
            }
        }

        public void Handle(QuestionAsked @event)
        {
            _logger.LogInformation("Handler for QuestionAsked executed");
            _questionsStore.Questions.Add(new QuestionData() 
            { 
                Id = @event.Id,
                AskedByUserName = @event.AskedBy,
                Content = @event.Content
            });
        }
    }
}