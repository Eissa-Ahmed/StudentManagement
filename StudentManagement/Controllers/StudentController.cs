using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagement.Context;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.Include(s => s.Department).ToListAsync();
            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "Id", "Name");
            ViewBag.Subjects = new SelectList(await _context.Subjects.ToListAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student, List<int> SubjectsIds)
        {
            if (ModelState.IsValid)
            {
                foreach (var subjectId in SubjectsIds)
                {
                    var studentSubject = new StudentSubject
                    {
                        StudentId = student.Id,
                        SubjectId = subjectId
                    };
                    student.StudentSubjects.Add(studentSubject);
                }
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "Id", "Name", student.DepartmentId);
            ViewBag.Subjects = new SelectList(await _context.Subjects.ToListAsync(), "Id", "Name");
            return View(student);
        }

        public async Task<IActionResult> Update(int id)
        {
            var student = await _context.Students.FirstAsync(s => s.Id == id);
            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "Id", "Name", student.DepartmentId);
            ViewBag.Subjects = new SelectList(await _context.Subjects.ToListAsync(), "Id", "Name");

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student, List<int> SubjectsIds)
        {
            throw new NotImplementedException();//
        }
    }
}
