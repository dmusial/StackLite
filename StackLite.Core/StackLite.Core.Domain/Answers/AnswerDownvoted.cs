using System;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Domain.Answers
{
    public class AnswerDownvoted : Event
    {
        public AnswerDownvoted(Guid answerId, string downvotedBy)
        {
            AnswerId = answerId;
            DownvotedBy = downvotedBy;
        }
        
        public Guid AnswerId { get; set; }
        public string DownvotedBy { get; set; }
    }
}