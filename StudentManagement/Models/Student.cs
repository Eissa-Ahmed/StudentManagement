

namespace StudentManagement.Models;

public class Student
{
    [Key] // Primary key
    public int Id { get; set; }

    [Required] // Name is required
    [StringLength(100)] // Maximum length of 100 characters
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress] // Validates email format
    public string Email { get; set; } = null!;

    [Required]
    public DateTime DateOfBirth { get; set; }

    // Foreign Key for Department
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    // Navigation property for Department
    public Department? Department { get; set; } = null;

    // Many-to-many relationship with Subject
    public List<StudentSubject> StudentSubjects { get; set; }
    public List<Subject> Subjects { get; set; }

    public Student()
    {
        StudentSubjects = new List<StudentSubject>();
        Subjects = new List<Subject>();
    }
}
