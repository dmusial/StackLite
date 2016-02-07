using System;
using System.Collections.Generic;

namespace StackLite.Core.FakeReportingStores
{
    public class QuestionData
    {
        public Guid Id { get; set; }
        public string AskedByUserName { get; set; }
        public string Content { get; set; }
    }
    
    public interface IQuestionsStore
    {
        List<QuestionData> Questions { get; }
    }
    
    public class QuestionsStore : IQuestionsStore
    {
        private List<QuestionData> _questions;
        
        public QuestionsStore()
        {
            _questions = new List<QuestionData>();
        }
        
        public List<QuestionData> Questions { get { return _questions; } }
    }
}