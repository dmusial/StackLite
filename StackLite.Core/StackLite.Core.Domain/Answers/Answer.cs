using System;
using System.Collections.Generic;
using StackLite.Core.Domain.Common;
using StackLite.Core.Domain.Questions;
using StackLite.Core.Domain.Users;

namespace StackLite.Core.Domain.Answers
{
    public class Answer : AggregateRoot
    {
        public Answer(IEnumerable<Event> events)
        {
            dynamic me = this as dynamic;
            foreach (var e in events) 
            {
                me.Apply((dynamic)e);
            }
        }
        
        internal Answer(Question question, string content, User answeredBy)
        {
            var id = Guid.NewGuid();
            
            var answerSuggested = new AnswerSuggested(id, question.Id, content, answeredBy.UserName);
            Apply(answerSuggested);
            Append(answerSuggested);
        }
        
        public Guid Id { get; private set; }
        public Guid ForQuestionId { get; private set; }
        public string AnsweredByUserName { get; private set; }
        public string Content { get; private set; }
        public int VoteScore { get; private set; }
        
        public void AmendContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException("content");
                
            var answerAmended = new AnswerAmended(Id, content);
            Apply(answerAmended);
            Append(answerAmended);
        }

        public void Upvote(User upvotedBy)
        {
            var answerUpvoted = new AnswerUpvoted(Id, upvotedBy.UserName);
            Apply(answerUpvoted);
            Append(answerUpvoted);
        }
        
        public void Downvote(User downvotedBy)
        {
            var asnwerDownvoted = new AnswerDownvoted(Id, downvotedBy.UserName);
            Apply(asnwerDownvoted);
            Append(asnwerDownvoted);
        }
        
        private void Apply(AnswerSuggested answerSuggested)
        {
            Id = answerSuggested.AnswerId;
            ForQuestionId = answerSuggested.QuestionId;
            Content = answerSuggested.Content;
            AnsweredByUserName = answerSuggested.AnsweredBy;
        }
        
        private void Apply(AnswerAmended answerAmended)
        {
            Content = answerAmended.Content;
        }
        
        private void Apply(AnswerUpvoted answerUpvoted)
        {
            VoteScore++;
        }
        
        private void Apply(AnswerDownvoted answerDownvoted)
        {
            VoteScore--;
        }
    }
}