using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagement.Context;

namespace StudentManagement.Controllers
{
    public class SubjectController : Controller
    {
        private readonly StudentContext _context;

        public SubjectController(StudentContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Subject> subjects = await _context.Subjects.Include(s => s.Teacher).ToListAsync();
            return View(subjects);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Subject model)
        {
            if (ModelState.IsValid)
            {
                await _context.Subjects.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "Id", "Name");
            return View();
        }

        public async Task<IActionResult> Details(int id)
            => View(await _context.Subjects.Include(s => s.Teacher).Include(s => s.Students)
                .FirstOrDefaultAsync(s => s.Id == id));

        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Subject? subject = await _context.Subjects.FindAsync(id);
            return View(subject);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Subject model)
        {
            if (ModelState.IsValid)
            {
                var subject = await _context.Subjects.FindAsync(model.Id);
                subject.Name = model.Name;
                _context.Subjects.Update(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
