using FakeBet.Models;
using System;

namespace FakeBet.Repository.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(Guid userId);

        void RegisterUser(User user);

        void DeactivateUser(Guid userId);

        void ActivateUser(Guid userId);

        void BanUser(Guid userId);
    }
}