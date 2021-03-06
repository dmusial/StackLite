using System;
using System.Collections.Generic;
using System.Linq;
using StackLite.Core.Persistance.ReadModels;

namespace StackLite.Core.Persistance
{
    public interface IQuestionsQuery
    {
        int AllQuestionsCount();
        QuestionData GetQuestionDetails(Guid questionId);
        List<QuestionData> AllQuestions();
    }

    public class QuestionsQuery : IQuestionsQuery
    {
        public int AllQuestionsCount()
        {
            using (var context = new ReadContext())
            {
                return context.Questions.Count();
            }
        }

        public QuestionData GetQuestionDetails(Guid questionId)
        {
            using (var context = new ReadContext())
            {
                return context.Questions.FirstOrDefault(q => q.Id == questionId);
            }
        }
          public List<QuestionData> AllQuestions()
        {
             using (var context = new ReadContext())
            {
                return context.Questions.ToList();
            }
        }
    }
}