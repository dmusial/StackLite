using System;
using StackLite.Core.Domain.Answers;
using StackLite.Core.Domain.Common;
using StackLite.Core.Domain.Questions;

namespace StackLite.Core.Domain.Users
{
    public class User : AggregateRoot
    {
        public User(string userName)
        {
            UserName = userName;
        }
        
        public string UserName { get; private set; }
        
        public Question Ask(string questionContent)
        {
            if (string.IsNullOrWhiteSpace(questionContent))
                throw new ArgumentException("questionContent");
                
            return new Question(this, questionContent);
        }
        
        public Answer SuggestAnswerTo(Question question, string answer)
        {
            if (question == null)
                throw new ArgumentNullException("question");
                
            if (string.IsNullOrWhiteSpace(answer))
                throw new ArgumentException("answer");
            
            return new Answer(question, answer, this);
        }
        
    }
}
