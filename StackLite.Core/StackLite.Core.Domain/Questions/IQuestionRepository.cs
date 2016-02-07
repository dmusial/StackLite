using System;

namespace StackLite.Core.Domain.Questions
{
    public interface IQuestionRepository
    {
        Question Get(Guid questionId);
        void Save(Question question);
    }
}