using System;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Domain.Questions
{
    public class QuestionAmended : Event
    {
        public QuestionAmended(Guid id, string content)
        {
            Id = id;
            Content = content;
        }
        
        public Guid Id { get; private set; }
        public string Content { get; private set; }
    }
}