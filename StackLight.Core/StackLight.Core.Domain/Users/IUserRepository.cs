using System;

namespace StackLight.Core.Domain.Users
{
    public interface IUserRepository
    {
        User Get(Guid userId);
        void Save();
    }
}