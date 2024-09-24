using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models;

public class Department
{
    [Key] // Primary key
    public int Id { get; set; }

    [Required] // Department name is required
    [StringLength(100)] // Maximum length of 100 characters
    public string Name { get; set; } = null!;

    // One-to-many relationship with Students and Teachers
    public List<Student> Students { get; set; }
    public List<Teacher> Teachers { get; set; }

    public Department()
    {
        Students = new List<Student>();
        Teachers = new List<Teacher>();
    }
}
