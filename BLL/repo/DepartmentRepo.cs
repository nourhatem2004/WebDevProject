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
            optionsBuilder.UseSqlServer("Server=NOURLAPTOP;Database=webdevproject;Trusted_Connection=True;TrustServerCertificate=True;")
            .EnableSensitiveDataLogging() // Show parameter values
            .LogTo(Console.WriteLine, LogLevel.Information);
            dbContext = new ApplicationDbContext(optionsBuilder.Options);
        }

        public IEnumerable<Department> GetAll()
        {
            return dbContext.Departments.ToList();
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

        public int Delete(int currentdept)
        {
            
            var employeesToMove = dbContext.Employees.Where(e => e.Dep_ID == currentdept).ToList();

            foreach (var Employee in employeesToMove)
            {
                Employee.Dep_ID = 15;
            }

            dbContext.SaveChanges();
            
            dbContext.Departments.Remove(dbContext.Departments.Find(currentdept));
            return dbContext.SaveChanges();
        }

    }
}