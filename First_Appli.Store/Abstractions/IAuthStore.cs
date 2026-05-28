using First_Appli.Common.Model;
using System.Threading.Tasks;

namespace First_Appli.Store.Abstractions
{
    public interface IAuthStore
    {
        Task Register(Employee employee);

        Task<Employee> GetEmployeeByEmail(string email);
    }
}