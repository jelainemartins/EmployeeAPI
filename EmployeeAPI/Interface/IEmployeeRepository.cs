using EmployeeAPI.Models;
using EmployeeAPI.ViewModel;

namespace EmployeeAPI.Interface
{
    //A interface da acesso ao [REPOSITORY];
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        List<Employee> AddList(List<Employee> employeeList);
        List<Employee> Get();
        Employee GetById(int id);
        List<Employee> GetBySex(char sex);
        List<Employee> GetByJob(string job);
        List<Employee> GetByBirthdate(DateTime date);
        List<Employee> GetPage(int pageNumber, int pageQuantity);
        Employee Update(int id, EmployeeViewModel employeeViewModel);
        void Remove(int id);
    }
}
