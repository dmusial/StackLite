using System.Linq;
using StackLite.Core.Domain.Questions;
using StackLite.Core.Persistance.EventHandlers;
using StackLite.Core.Persistance.ReadModels;

namespace StackLite.Core.Projections.Handlers
{
    public interface IQuestionHandler
    {
        void Handle(QuestionAsked @event);
        void Handle(QuestionAmended @event);
    }
    
    public class QuestionHandler : IQuestionHandler, Handles<QuestionAsked>, Handles<QuestionAmended>
    {
        public void Handle(QuestionAmended @event)
        {
            using (var context = new ReadContext())
            {
                var question = context.Questions.FirstOrDefault(q => q.Id == @event.Id);
                if (question != null)
                {
                    question.Content = @event.Content;
                    context.SaveChanges();
                }
            }
        }

        public void Handle(QuestionAsked @event)
        {
            using (var context = new ReadContext())
            {
                context.Questions.Add(new QuestionData() 
                { 
                    Id = @event.Id,
                    AskedByUserName = @event.AskedBy,
                    Content = @event.Content
                });
                
                context.SaveChanges();
            }
        }
    }
}