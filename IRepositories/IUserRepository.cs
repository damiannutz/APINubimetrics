using Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IUserRepository
    {
        Task<bool> Delete(int id);
        Task<User> Save(User user);
        Task<bool> UpdatePassword(int id, string newPassword);
        Task<List<User>> Get(int id, string nombre, string apellido, string email);
    }
}
