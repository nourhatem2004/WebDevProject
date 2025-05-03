using System.Collections.Generic;
using WebDevProject.BLL.interfaces;
using WebDevProject.Data;
using WebDevProject.Models;

namespace WebDevProject.BLL.repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ApplicationDbContext applicationDbContext;

        public EmployeeRepo()
        {
            applicationDbContext = new ApplicationDbContext();
        }
        public int Add(Employee employee)
        {
            applicationDbContext.Employees.Add(employee);
            return applicationDbContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            applicationDbContext.Employees.Remove(employee);
            return applicationDbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return applicationDbContext.Employees.ToList();
        }

        public Employee? GetById(int id)
        {
            return applicationDbContext.Employees.Find(id);
        }

        public int Update(Employee employee)
        {
            applicationDbContext.Employees.Update(employee);
            return applicationDbContext.SaveChanges();
        }
    }
}
