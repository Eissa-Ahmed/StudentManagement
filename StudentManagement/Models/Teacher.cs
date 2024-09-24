using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models;

public class Teacher
{
    [Key] // Primary key
    public int Id { get; set; }

    [Required] // Name is required
    [StringLength(100)] // Maximum length of 100 characters
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress] // Validates email format
    public string Email { get; set; } = null!;

    // Foreign Key for Department
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    // Navigation property for Department
    public Department Department { get; set; } = null!;

    // One-to-many relationship with Subject
    public List<Subject> Subjects { get; set; }

    public Teacher()
    {
        Subjects = new List<Subject>();
    }
}
