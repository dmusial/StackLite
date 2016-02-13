using System;
using System.Collections.Generic;

namespace StackLite.Core.FakeReportingStores
{
    public class AnswerData
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string AnsweredBy { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
    }
    
    public interface IAnswersStore
    {
        List<AnswerData> Answers { get; }
    }

    public class AnswersStore : IAnswersStore
    {
         private List<AnswerData> _answers;
        
        public AnswersStore()
        {
            _answers = new List<AnswerData>();
        }
        
        public List<AnswerData> Answers { get { return _answers; } }
    }
}