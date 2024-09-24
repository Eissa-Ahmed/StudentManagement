using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models;

public class StudentSubject
{
    // Foreign Key for Student
    [ForeignKey("Student")]
    public int StudentId { get; set; }

    // Navigation property for Student
    public Student Student { get; set; } = null!;

    // Foreign Key for Subject
    [ForeignKey("Subject")]
    public int SubjectId { get; set; }

    // Navigation property for Subject
    public Subject Subject { get; set; } = null!;
}