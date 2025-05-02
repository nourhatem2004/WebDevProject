using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDevProject.Models;

namespace WebDevProject.BLL.interfaces
{
    public interface IDepartmentRepo
    {
        IEnumerable<Department> GetAll();
        int Add(Department d);
        int Update(Department d);
        int Delete(int id);
        
    }
}