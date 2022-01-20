using Entities;
using IRepositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _userRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<User> Save(User user)
        {
            try
            {
                return await _userRepository.Save(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdatePassword(int id, string newPassword)
        {
            try
            {
                return await _userRepository.UpdatePassword(id, newPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<User>> Get(int id, string nombre, string apellido, string email)
        {
            try
            {
                return await _userRepository.Get(id, nombre, apellido, email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
