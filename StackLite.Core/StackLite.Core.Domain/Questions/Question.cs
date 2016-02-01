using System;
using StackLite.Core.Domain.Users;

namespace StackLite.Core.Domain.Questions
{
    public class Question 
    {
        internal Question(User askedBy, string content)
        {
            Id = Guid.NewGuid();
            AskedByUserName = askedBy.UserName;
            Content = content;
        }
        
        public Guid Id { get; private set; }
        public string AskedByUserName { get; private set; }
        public string Content { get; private set; }
    }
}