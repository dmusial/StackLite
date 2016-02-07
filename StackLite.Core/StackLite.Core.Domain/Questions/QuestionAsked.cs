using System;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Domain.Questions
{
    public class QuestionAsked : Event
    {
        public QuestionAsked(Guid id, string askedBy, string content)
        {
            Id = id;
            AskedBy = askedBy;
            Content = content;
        }
        
        public Guid Id { get; private set; }
        public string AskedBy { get; private set; }
        public string Content { get; private set; }
    }
}