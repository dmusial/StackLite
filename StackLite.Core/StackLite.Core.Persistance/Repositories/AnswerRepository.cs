using System;
using System.Collections.Generic;
using System.Linq;
using StackLite.Core.Domain.Answers;

namespace StackLite.Core.Persistance
{
    public class AnswerRepository : IAnswerRepository
    {
        private List<Answer> _answers;
        public AnswerRepository()
        {
            _answers = new List<Answer>();
        }

        public void Add(Answer answer)
        {
            _answers.Add(answer);
        }

        public Answer Get(Guid answerId)
        {
            return _answers.FirstOrDefault(a => a.Id == answerId);
        }

        public List<Answer> GetAllFor(Guid questionId)
        {
            return _answers.Where(a => a.ForQuestionId == questionId).ToList();    
        }

        public void Save()
        {
            
        }
    }
}