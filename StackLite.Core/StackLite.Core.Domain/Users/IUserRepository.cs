using System;

namespace StackLite.Core.Domain.Users
{
    public interface IUserRepository
    {
        User Get(Guid userId);
        void Save();
    }
}