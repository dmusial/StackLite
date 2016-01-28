using System;
using System.Collections.Generic;
using System.Linq;
using StackLight.Core.Domain.Questions;

namespace StackLight.Core.Persistance
{    
    public class QuestionRepository : IQuestionRepository
    {
        private List<Question> _questions;
    
        public QuestionRepository()
        {
            _questions = new List<Question>();            
        }
        public void Add(Question question)
        {
            _questions.Add(question);
        }

        public Question Get(Guid questionId)
        {
            return _questions.FirstOrDefault(q => q.Id == questionId);
        }

        public int GetAllQuestionsCount()
        {
            return _questions.Count();
        }

        public void Save()
        {
            
        }
    }
}
