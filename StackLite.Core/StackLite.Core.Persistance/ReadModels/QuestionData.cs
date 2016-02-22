using System;

namespace StackLite.Core.Persistance.ReadModels
{
    public class QuestionData
    {
        public Guid Id { get; set; }
        public string AskedByUserName { get; set; }
        public string Content { get; set; }
    }
}