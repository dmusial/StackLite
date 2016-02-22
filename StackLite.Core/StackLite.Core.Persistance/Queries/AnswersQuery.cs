using System;
using System.Collections.Generic;
using System.Linq;
using StackLite.Core.Persistance.ReadModels;

namespace StackLite.Core.Persistance
{
    public interface IAnswersQuery
    {
        List<AnswerData> GetAllForQuestion(Guid questionId);
    }

    public class AnswersQuery : IAnswersQuery
    {
        public List<AnswerData> GetAllForQuestion(Guid questionId)
        {
            using (var context = new ReadContext())
            {
                return context.Answers.Where(a => a.QuestionId == questionId).ToList();
            }
        }
    }
}