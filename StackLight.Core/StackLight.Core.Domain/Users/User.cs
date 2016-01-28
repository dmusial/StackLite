using StackLight.Core.Domain.Answers;
using StackLight.Core.Domain.Questions;

namespace StackLight.Core.Domain.Users
{
    public class User
    {
        public User(string userName)
        {
            UserName = userName;
        }
        
        public string UserName { get; private set; }
        
        public Question Ask(string questionContent)
        {
            return new Question(this, questionContent);
        }
        
        public Answer SuggestAnswerTo(Question question, string answer)
        {
            return new Answer(question, answer, this);
        }
    }
}
