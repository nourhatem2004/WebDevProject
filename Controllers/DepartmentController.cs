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

        public IActionResult Delete(int id)
        {

            depRepo.Delete(id);
            

            return RedirectToAction("getallview");
        }


    }
}