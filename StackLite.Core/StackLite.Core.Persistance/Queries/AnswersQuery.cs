using System;
using System.Collections.Generic;
using System.Linq;
using StackLite.Core.FakeReportingStores;

namespace StackLite.Core.Persistance
{
    public interface IAnswersQuery
    {
        List<AnswerData> GetAllForQuestion(Guid questionId);
    }

    public class AnswersQuery : IAnswersQuery
    {
        private readonly IAnswersStore _answersStore;
        
        public AnswersQuery(IAnswersStore answersStore)
        {
            _answersStore = answersStore;
        }
        
        public List<AnswerData> GetAllForQuestion(Guid questionId)
        {
            return _answersStore.Answers.Where(a => a.QuestionId == questionId).ToList();
        }
    }
}