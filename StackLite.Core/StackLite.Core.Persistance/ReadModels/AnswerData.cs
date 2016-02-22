using System;

namespace StackLite.Core.Persistance.ReadModels
{
    public class AnswerData
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string AnsweredBy { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
    }
}