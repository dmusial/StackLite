using System;
using System.Collections.Generic;
using StackLite.Core.Domain.Common;
using StackLite.Core.Domain.Users;

namespace StackLite.Core.Domain.Questions
{
    public class Question : AggregateRoot
    {
        public Question(IEnumerable<Event> events)
        {
            dynamic me = this as dynamic;
            foreach (var e in events) 
            {
                me.Apply((dynamic)e);
            }
        }
        
        internal Question(User askedBy, string content)
        {
            var id = Guid.NewGuid();
            
            var questionAsked = new QuestionAsked(id, askedBy.UserName, content);
            Apply(questionAsked);
            Append(questionAsked);
        }
        
        public Guid Id { get; private set; }
        public string AskedByUserName { get; private set; }
        public string Content { get; private set; }
        
        public void AmendContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException("content");
            
            var questionAmended = new QuestionAmended(Id, content);
            Apply(questionAmended);
            Append(questionAmended);
        }

        private void Apply(QuestionAsked questionAsked)
        {
            Id = questionAsked.Id;
            AskedByUserName = questionAsked.AskedBy;
            Content = questionAsked.Content;
        }
        
        private void Apply(QuestionAmended questionAmended)
        {
            Content = questionAmended.Content;
        }
    }
}