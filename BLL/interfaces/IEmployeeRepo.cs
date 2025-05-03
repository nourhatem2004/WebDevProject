using System.Collections.Generic;
using WebDevProject.Models;

namespace WebDevProject.BLL.interfaces
{
    public interface IEmployeeRepo
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        int Add(Employee employee);
        int Update(Employee employee);
        int Delete(Employee employee);
    }
}
