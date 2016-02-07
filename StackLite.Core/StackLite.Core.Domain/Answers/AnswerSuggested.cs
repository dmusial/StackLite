using System;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Domain.Answers
{
    public class AnswerSuggested : Event
    {
        public AnswerSuggested(Guid answerId, Guid questionId, string content, string answeredBy)
        {
            AnswerId = answerId;
            QuestionId = questionId;
            Content = content;
            AnsweredBy = answeredBy;
        }
        
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
        public string AnsweredBy { get; set; }
    } 
    
}