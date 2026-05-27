using Microsoft.Data.SqlClient;
using First_Appli.Common.Model;
using First_Appli.Common.utilities;
using First_Appli.Store.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace First_Appli.Store.Implementations
{
    public class EmployeeStore : IEmployeeStore
    {
        private readonly string _connectionString;

        public EmployeeStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(AppConstants.GetAllEmployees, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await con.OpenAsync();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            Employee employee = new Employee()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                RoleId = reader["RoleId"].ToString(),
                                Name = reader["Name"].ToString(),
                                Department = reader["Department"].ToString()
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return employees;
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee employee = new Employee();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(AppConstants.GetEmployeeById, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(AppConstants.Id, id);

                        await con.OpenAsync();

                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            employee.Id = Convert.ToInt32(reader["Id"]);
                            employee.RoleId = reader["RoleId"].ToString();
                            employee.Name = reader["Name"].ToString();
                            employee.Department = reader["Department"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return employee;
        }
        public async Task<int> InsertEmployee(Employee employee)
        {
            int result = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(AppConstants.InsertEmployee, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(AppConstants.Name, employee.Name);
                        cmd.Parameters.AddWithValue(AppConstants.Department, employee.Department);

                        await con.OpenAsync();

                        result = await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public async Task<int> UpdateEmployee(Employee employee)
        {
            int result = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(AppConstants.UpdateEmployee, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(AppConstants.Id, employee.Id);
                        cmd.Parameters.AddWithValue(AppConstants.Name, employee.Name);
                        cmd.Parameters.AddWithValue(AppConstants.Department, employee.Department);

                        await con.OpenAsync();

                        result = await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public async Task<int> DeleteEmployee(int id)
        {
            int result = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(AppConstants.DeleteEmployee, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(AppConstants.Id, id);

                        await con.OpenAsync();

                        result = await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}