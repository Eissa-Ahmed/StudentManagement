namespace StudentManagement.Models;

public class Subject
{
    [Key] // Primary key
    public int Id { get; set; }

    [Required] // Subject name is required
    [StringLength(100)] // Maximum length of 100 characters
    public string Name { get; set; } = null!;

    // Foreign Key for Teacher (a subject is taught by a teacher)
    [ForeignKey("Teacher")]
    public int TeacherId { get; set; }

    // Navigation property for Teacher
    public Teacher? Teacher { get; set; } = null;

    // Many-to-many relationship with Student
    public List<StudentSubject> StudentSubjects { get; set; }
    public List<Student> Students { get; set; }

    public Subject()
    {
        StudentSubjects = new List<StudentSubject>();
        Students = new List<Student>();
    }
}
