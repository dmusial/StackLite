using System;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Domain.Answers
{
    public class AnswerAmended : Event
    {
        public AnswerAmended(Guid answerId, string content)
        {
            AnswerId = answerId;
            Content = content;
        }
        
        public Guid AnswerId { get; set; }
        public string Content { get; set; }
    } 
}