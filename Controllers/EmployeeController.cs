using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDevProject.BLL.repo;
using WebDevProject.Models;

namespace WebDevProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepo repo;
        private readonly DepartmentRepo depRepo;
        public EmployeeController()
        {
            repo = new EmployeeRepo();
            depRepo = new DepartmentRepo();
        }
        public IActionResult Index()
        {
            var employee = repo.GetAll();
            return View(employee);
        }

        public IActionResult Create()
        {
            var departments = depRepo.GetAll();

            if (departments == null || !departments.Any())
            {
                // throw new Exception("No departments found");

                ViewBag.Departments = new List<SelectListItem>(); // Prevent null
            }
            else
            {
                ViewBag.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.ID.ToString(),
                    Text = d.Dep_name
                }).ToList();
            }

            return View();
        }




        [HttpPost]
        public IActionResult Create(CreateEmployeeDto empDto)
        {
            if (ModelState.IsValid)
            {
                var emp = new Employee()
                {
                    Fname = empDto.Fname,
                    Lname = empDto.Lname,
                    Age = empDto.Age,
                    Salary = empDto.Salary,
                    Dep_ID = empDto.Dep_ID
                    
                };

                var count = repo.Add(emp);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(empDto);
        }

        public IActionResult Details(int id)
        {
            var emp = repo.GetById(id);
            return View(emp);
        }

        public IActionResult Delete(int id)
        {
            var emp = repo.GetById(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Delete(Employee emp)
        {
            var count = repo.Delete(emp);
            if (count > 0)
            {
                return RedirectToAction("Index");
            }
            return View(emp);
        }
        public IActionResult Edit(int id)
        {
            var departments = depRepo.GetAll();

            if (departments == null || !departments.Any())
            {
                // throw new Exception("No departments found");

                ViewBag.Departments = new List<SelectListItem>(); // Prevent null
            }
            else
            {
                ViewBag.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.ID.ToString(),
                    Text = d.Dep_name
                }).ToList();
            }

            var emp = repo.GetById(id);
            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            var count = repo.Update(emp);
            if (count > 0)
            {
                return RedirectToAction("Index");
            }
            return View(emp);
        }


    }
}
