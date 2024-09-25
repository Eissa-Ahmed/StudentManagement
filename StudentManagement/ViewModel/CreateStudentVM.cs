using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.ViewModel
{
    public class CreateStudentVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        [Remote("NameIsUnique", "Student", ErrorMessage = "Name already exists")]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Remote("EmailIsUnique", "Student", ErrorMessage = "Email already exists")]
        public string Email { get; set; } = null!;
        [Required]
        [Range(18, 22, ErrorMessage = "Age must be between 18 and 22")]
        public string DateOfBirth { get; set; } = null!;
        [Required(ErrorMessage = "Department is required")]
        public string DepartmentId { get; set; } = null!;
        [Required(ErrorMessage = "At least one subject is required")]
        public List<int> SubjectsIds { get; set; } = null!;
    }
}


