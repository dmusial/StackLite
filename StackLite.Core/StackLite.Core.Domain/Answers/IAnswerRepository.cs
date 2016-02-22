using System;

namespace StackLite.Core.Domain.Answers
{
    public interface IAnswerRepository
    {
        Answer Get(Guid answerId);
        void Save(Answer answer);
    }
}