using System;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Domain.Answers
{
    public class AnswerUpvoted : Event
    {
        public AnswerUpvoted(Guid answerId, string upvotedBy)
        {
            AnswerId = answerId;
            UpvotedBy = upvotedBy;
        }
        
        public Guid AnswerId { get; private set; }
        public string UpvotedBy { get; private set; }
    }
}