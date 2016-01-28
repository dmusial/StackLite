using System;
using StackLight.Core.Domain.Questions;
using StackLight.Core.Domain.Users;

namespace StackLight.Core.Domain.Answers
{
    public class Answer
    {
        public Answer(Question question, string content, User answeredBy)
        {
            Id = Guid.NewGuid();
            ForQuestionId = question.Id;
            Content = content;
            AnsweredByUserName = answeredBy.UserName;
        }
        
        public Guid Id { get; private set; }
        public Guid ForQuestionId { get; private set; }
        public string AnsweredByUserName { get; private set; }
        public string Content { get; private set; }
        public int VoteScore { get; private set; }
        
        public void Upvote(User upvotedBy)
        {
            VoteScore++;
        }
        
        public void Downvote(User downvotedBy)
        {
            VoteScore--;
        }
    }
}