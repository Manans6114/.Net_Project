using First_Appli.Common.Model;
using First_Appli.Service.Abstractions;
using First_Appli.Store.Abstractions;

namespace First_Appli.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeStore _employeeStore;

        public EmployeeService(IEmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _employeeStore.GetAllEmployees();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeStore.GetEmployeeById(id);
        }

        public async Task<int> InsertEmployee(Employee employee)
        {
            return await _employeeStore.InsertEmployee(employee);
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            return await _employeeStore.UpdateEmployee(employee);
        }

        public async Task<int> DeleteEmployee(int id)
        {
            return await _employeeStore.DeleteEmployee(id);
        }
    }
}