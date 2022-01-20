using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IUserService
    {
        Task<bool> Delete(int id);
        Task<User> Save(User user);
        Task<bool> UpdatePassword(int id, string newPassword);
        Task<List<User>> Get(int id, string nombre, string apellido, string email);

    }
}
