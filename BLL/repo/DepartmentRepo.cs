using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebDevProject.Models;
using WebDevProject.Data;
using WebDevProject.BLL.interfaces;


namespace WebDevProject.BLL.repo
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly ApplicationDbContext dbContext;

        public DepartmentRepo()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=MALAKS-LAPTOP;Database=WebDevProjectDB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;")
            .EnableSensitiveDataLogging() // Show parameter values
            .LogTo(Console.WriteLine, LogLevel.Information);
            dbContext = new ApplicationDbContext(optionsBuilder.Options);
        }

        public IEnumerable<Department> GetAll()
        {
            return dbContext.Departments.ToList();
        }
        public Department? GetById(int id)
        {
            return dbContext.Departments.Find(id); //use find for better performance
        }

        public int Add(Department d)
        {
            dbContext.Departments.Add(d);
            return dbContext.SaveChanges();
        }

        public int Update(Department d)
        {
            dbContext.Departments.Update(d);
            return dbContext.SaveChanges();
        }

        public int Delete(int currentDeptId)
        {
            var department = dbContext.Departments.Find(currentDeptId);
            if (department == null)
                return 0;

            var fallbackDept = dbContext.Departments.Find(15);
            if (fallbackDept == null)
                throw new Exception("Fallback department with ID 15 does not exist.");

            var employeesToMove = dbContext.Employees
                .Where(e => e.Dep_ID == currentDeptId)
                .ToList();

            foreach (var employee in employeesToMove)
            {
                employee.Dep_ID = fallbackDept.ID;
            }

            dbContext.SaveChanges();
            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges();
        }

    }
}