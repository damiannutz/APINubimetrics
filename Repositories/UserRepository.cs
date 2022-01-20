using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Entities.Helpers;
using Entities;
using IRepositories;
using System.Security.Cryptography;

namespace Repositories
{
    public class UserRepository: IUserRepository
    {
        private IConfiguration _configuration;
        private string _connectionString { get; set; }

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(Constants.NUBIMETRICS_DB_CONNECTION_NAME);
        }

        public async Task<bool> Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();   
                string procedure = _configuration["Procedures:DeleteUser"];

                using (var command = new SqlCommand(procedure, connection))
                {
                    try
                    {
                        
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", id);
                        
                        await command.ExecuteNonQueryAsync(); 

                    }
                    catch (Exception Ex)
                    {
                        await connection.CloseAsync();
                        throw Ex;
                    }
                }
            }
            return true;
        }

        public async Task<User> Save(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();   
                string procedure = _configuration["Procedures:SaveUser"];

                using (var command = new SqlCommand(procedure, connection))
                {
                    try
                    {
                        //solo paso el password cuando inserto
                        string hashedPassword = user.Id == 0? SHA512Generator.HashText(user.Password) : string.Empty;

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", user.Id);
                        command.Parameters.AddWithValue("@nombre", user.Nombre);
                        command.Parameters.AddWithValue("@apellido", user.Apellido);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@password", hashedPassword);

                        SqlDataReader rdr = await command.ExecuteReaderAsync(); 

                        while (rdr.Read())
                        {
                            user.Id = rdr.GetInt32("ID");
                            string error = rdr.GetString("ERROR");
                            if (!string.IsNullOrWhiteSpace(error))
                                throw new Exception(error);
                        }
                        await rdr.CloseAsync();
                    }
                    catch (Exception Ex)
                    {
                        await connection.CloseAsync();
                        throw Ex;
                    }
                }
            }
            return user;
        }

        public async Task<bool> UpdatePassword(int id, string newPassword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();   
                string procedure = _configuration["Procedures:SaveUser"];

                using (var command = new SqlCommand(procedure, connection))
                {
                    try
                    {
                        //solo paso el password cuando inserto
                        string hashedPassword = SHA512Generator.HashText(newPassword) ;

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nombre", string.Empty);
                        command.Parameters.AddWithValue("@apellido", string.Empty);
                        command.Parameters.AddWithValue("@email", string.Empty);
                        command.Parameters.AddWithValue("@password", hashedPassword);

                        SqlDataReader rdr = await command.ExecuteReaderAsync();

                        while (rdr.Read())
                        {
                            string error = rdr.GetString("ERROR");
                            if (!string.IsNullOrWhiteSpace(error))
                                throw new Exception(error);
                        }
                        await rdr.CloseAsync();
                    }
                    catch (Exception Ex)
                    {
                        await connection.CloseAsync();
                        throw Ex;
                    }
                }
            }
            return true;
        }

        public async Task<List<User>> Get(int id, string nombre, string apellido, string email)
        {
            List<User> response = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();   
                string procedure = _configuration["Procedures:GetUsers"];
        
                using (var command = new SqlCommand(procedure, connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@apellido", apellido);
                        command.Parameters.AddWithValue("@email", email);

                        SqlDataReader rdr = await command.ExecuteReaderAsync();  

                        while (rdr.Read())
                        {
                            response.Add(Make(rdr));
                        }
                        await rdr.CloseAsync();
                    }
                    catch (Exception Ex)
                    {
                        await connection.CloseAsync();
                        throw Ex;
                    }
                }
            }
            return response;
        }

        private User Make(SqlDataReader dr)
        {
            return new User()
            {
                Id = dr.GetInt32("ID"),
                Nombre = dr.GetString("NOMBRE"),
                Apellido = dr.GetString("APELLIDO"),
                Email = dr.GetString("EMAIL"),
                Password = dr.GetString("PASSWORD")
            };
        }

    }
}
