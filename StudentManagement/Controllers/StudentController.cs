﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagement.Context;
using StudentManagement.ViewModel;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _context;
        private readonly IMapper _mapperr;

        public StudentController(StudentContext context, IMapper mapperr)
        {
            _context = context;
            _mapperr = mapperr;
        }

        public async Task<IActionResult> NameIsUnique(string Name)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Name == Name);
            return Json(student == null);
        }
        public async Task<IActionResult> EmailIsUnique(string Email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == Email);
            return Json(student == null);
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
        public async Task<IActionResult> Create(CreateStudentVM model)
        {
            if (ModelState.IsValid)
            {
                var student = _mapperr.Map<Student>(model);
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "Id", "Name", model.DepartmentId);
            ViewBag.Subjects = new SelectList(await _context.Subjects.ToListAsync(), "Id", "Name", model.SubjectsIds);
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var student = await _context.Students.FirstAsync(s => s.Id == id);
            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "Id", "Name", student.DepartmentId);
            ViewBag.Subjects = new SelectList(await _context.Subjects.ToListAsync(), "Id", "Name");

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult AddSubjects(int studentId)
        {
            var student = _context.Students
                                  .Where(s => s.Id == studentId)
                                  .Select(s => new StudentSubjectsViewModel
                                  {
                                      StudentId = s.Id,
                                      StudentName = s.Name,
                                      SelectedSubjectIds = s.StudentSubjects.Select(ss => ss.SubjectId).ToList(),
                                      AvailableSubjects = _context.Subjects.ToList() // جلب جميع المواد
                                  })
                                  .FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [HttpPost]
        public IActionResult AddSubjects(StudentSubjectsViewModel model)
        {
            var student = _context.Students
                                  .Where(s => s.Id == model.StudentId)
                                  .FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }

            // حذف المواد القديمة من الطالب
            var oldSubjects = _context.StudentSubjects
                                      .Where(ss => ss.StudentId == model.StudentId)
                                      .ToList();
            _context.StudentSubjects.RemoveRange(oldSubjects);

            // إضافة المواد الجديدة
            foreach (var subjectId in model.SelectedSubjectIds)
            {
                var newStudentSubject = new StudentSubject
                {
                    StudentId = model.StudentId,
                    SubjectId = subjectId
                };
                _context.StudentSubjects.Add(newStudentSubject);
            }

            _context.SaveChanges();

            return RedirectToAction("Details", new { id = model.StudentId });
        }
    }
}
