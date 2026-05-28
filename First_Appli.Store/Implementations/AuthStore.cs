using System;
using System.Collections.Generic;
using System.Text;
using First_Appli.Common.Model;
using First_Appli.Store.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;



namespace First_Appli.Store.Implementations
{
    public class AuthStore : IAuthStore
    {
        private readonly string _connectionString;

        public AuthStore(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Register(Employee employee)
        {
            using SqlConnection connection =
                new SqlConnection(_connectionString);
            using (SqlCommand command =
                new SqlCommand("sp_RegisterEmployee", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", employee.Name);

                command.Parameters.AddWithValue("@Department", employee.Department);

                command.Parameters.AddWithValue("@Email", employee.Email);

                command.Parameters.AddWithValue("@PasswordHash", employee.PasswordHash);

                command.Parameters.AddWithValue("@Role", employee.Role);

                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            Employee employee = null;

            using (SqlConnection connection =
                new SqlConnection(_connectionString))
            {
                using (SqlCommand command =
                    new SqlCommand("sp_GetEmployeeByEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);

                    await connection.OpenAsync();

                    SqlDataReader reader =
                        await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        employee = new Employee
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Department = reader["Department"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
            }

            return employee;
        }
    }
}