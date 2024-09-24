using Microsoft.AspNetCore.Mvc;
using StudentManagement.Context;

namespace StudentManagement.Controllers
{
    public class TeacherController : Controller
    {
        private readonly StudentContext _context;

        public TeacherController(StudentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _context.Teachers.Include(t => t.Department).ToListAsync();
            return View(teachers);
        }
    }
}
