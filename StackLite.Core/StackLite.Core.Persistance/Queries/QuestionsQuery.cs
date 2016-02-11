using System;
using System.Collections.Generic;
using System.Linq;
using StackLite.Core.FakeReportingStores;

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
        private readonly IQuestionsStore _store;
        
        public QuestionsQuery(IQuestionsStore store)
        {
            _store = store;
        }
        
        public int AllQuestionsCount()
        {
            return _store.Questions.Count;
        }

        public QuestionData GetQuestionDetails(Guid questionId)
        {
            return _store.Questions.FirstOrDefault(q => q.Id == questionId);
        }
        
        public List<QuestionData> AllQuestions()
        {
            return _store.Questions;
        }

    }
}