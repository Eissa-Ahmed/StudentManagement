using Microsoft.AspNetCore.Mvc;
using StudentManagement.Context;

namespace StudentManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly StudentContext _context;

        public DepartmentController(StudentContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Department> departments = await _context.Departments.ToListAsync();
            return View(departments);
        }
    }
}
