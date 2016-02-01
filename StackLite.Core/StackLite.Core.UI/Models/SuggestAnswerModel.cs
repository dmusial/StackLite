using System;

namespace StackLite.Core.UI.Models
{
    public class SuggestAnswerModel
    {
        public Guid QuestionId { get; set; }
        public string AnswerContent { get; set; }
    }
}