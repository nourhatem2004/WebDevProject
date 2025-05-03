using Microsoft.AspNetCore.Mvc;
using WebDevProject.BLL.repo;
using WebDevProject.Models;

namespace WebDevProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentRepo depRepo;

        public DepartmentController()
        {
            depRepo = new DepartmentRepo();
        }

        public IActionResult getallview()
        {
            var departments = depRepo.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Dep_name = departmentDto.Dep_name,
                    establish_date = departmentDto.establish_date,
                };
                var count = depRepo.Add(department);
                if (count > 0)
                {
                    return RedirectToAction("getallview");
                }
            }
            return View(departmentDto);
        }

        public IActionResult Details(int id)
        {
            var department = depRepo.GetById(id);
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = depRepo.GetById(id);
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                var count = depRepo.Update(department);
                if (count > 0)
                {
                    return RedirectToAction("getallview");
                }
            }
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            var department = depRepo.GetById(id);
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            var count = depRepo.Delete(department.ID);
            if (count > 0)
            {
                return RedirectToAction("getallview");
            }
            return View(department);

        }
    }
}