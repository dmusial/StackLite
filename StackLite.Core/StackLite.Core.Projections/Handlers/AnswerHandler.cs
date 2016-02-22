using System.Linq;
using StackLite.Core.Domain.Answers;
using StackLite.Core.Persistance.EventHandlers;
using StackLite.Core.Persistance.ReadModels;

namespace StackLite.Core.Projections.Handlers
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
        public AnswerHandler()
        {
        }

        public void Handle(AnswerSuggested @event)
        {
            using (var context = new ReadContext())
            {
                var answer = new AnswerData()
                {
                    Id = @event.AnswerId,
                    QuestionId = @event.QuestionId,
                    AnsweredBy = @event.AnsweredBy,
                    Content = @event.Content
                };
                
                context.Answers.Add(answer);
                context.SaveChanges();
            }
        }

        public void Handle(AnswerAmended @event)
        {
            using (var context = new ReadContext())
            {
                var answer = context.Answers.FirstOrDefault(a => a.Id == @event.AnswerId);
                if (answer != null)
                {
                    answer.Content = @event.Content;
                    context.SaveChanges();
                }
            }
        }
        
        public void Handle(AnswerUpvoted @event)
        {
            using (var context = new ReadContext())
            {
                var answer = context.Answers.FirstOrDefault(a => a.Id == @event.AnswerId);
                if (answer != null)
                {
                    answer.Votes++;
                    context.SaveChanges();
                }
            }
        }

        public void Handle(AnswerDownvoted @event)
        {
            using (var context = new ReadContext())
            {
                var answer = context.Answers.FirstOrDefault(a => a.Id == @event.AnswerId);
                if (answer != null)
                {
                    answer.Votes--;
                    context.SaveChanges();
                }
            }
        }
    }
}