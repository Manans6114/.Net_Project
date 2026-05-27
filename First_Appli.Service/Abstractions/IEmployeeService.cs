using System;
using System.Collections.Generic;
using System.Text;
using First_Appli.Common.Model;

using First_Appli.Common.Model;

namespace First_Appli.Service.Abstractions
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployees();

        Task<Employee> GetEmployeeById(int id);

        Task<int> InsertEmployee(Employee employee);

        Task<int> UpdateEmployee(Employee employee);

        Task<int> DeleteEmployee(int id);
    }
}