using System;

namespace StackLight.Core.Domain.Answers
{
    public interface IAnswerRepository
    {
        Answer Get(Guid answerId);
        void Save();
    }
}