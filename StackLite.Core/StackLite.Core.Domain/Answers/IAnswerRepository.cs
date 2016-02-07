using System;
using System.Collections.Generic;

namespace StackLite.Core.Domain.Answers
{
    public interface IAnswerRepository
    {
        Answer Get(Guid answerId);
        List<Answer> GetAllFor(Guid questionId);
        void Save(Answer answer);
    }
}