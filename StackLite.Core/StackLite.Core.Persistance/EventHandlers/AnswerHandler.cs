using System.Linq;
using StackLite.Core.Domain.Answers;
using StackLite.Core.FakeReportingStores;

namespace StackLite.Core.EventHandlers
{
    public interface IAnswerHandler
    {
        void Handle(AnswerSuggested @event);
        void Handle(AnswerAmended @event);
        void Handle(AnswerUpvoted @event);
        void Handle(AnswerDownvoted @event);
    }


    public class AnswerHandler : IAnswerHandler, Handles<AnswerSuggested>, Handles<AnswerAmended>, Handles<AnswerUpvoted>, Handles<AnswerDownvoted>
    {
        private readonly IAnswersStore _answerStore;

        public AnswerHandler(IAnswersStore answerStore)
        {
            _answerStore = answerStore;
        }

        public void Handle(AnswerSuggested @event)
        {
            var answer = new AnswerData()
            {
                Id = @event.AnswerId,
                QuestionId = @event.QuestionId,
                AnsweredBy = @event.AnsweredBy,
                Content = @event.Content
            };
            
            _answerStore.Answers.Add(answer);
        }

        public void Handle(AnswerAmended @event)
        {
            var answer = _answerStore.Answers.FirstOrDefault(a => a.Id == @event.AnswerId);
            if (answer != null)
            {
                answer.Content = @event.Content;
            }
        }
        
        public void Handle(AnswerUpvoted @event)
        {
            var answer = _answerStore.Answers.FirstOrDefault(a => a.Id == @event.AnswerId);
            if (answer != null)
            {
                answer.Votes++;
            }
        }

        public void Handle(AnswerDownvoted @event)
        {
            var answer = _answerStore.Answers.FirstOrDefault(a => a.Id == @event.AnswerId);
            if (answer != null)
            {
                answer.Votes--;
            }
        }
    }
}