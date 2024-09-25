using StudentManagement.Context;

namespace StudentManagement.CustomValidation;

public class UniqeNameAttribute : ValidationAttribute
{
    private readonly StudentContext _context;

    public UniqeNameAttribute(StudentContext context)
    {
        _context = context;
    }

    public override string FormatErrorMessage(string name)
    {
        return base.FormatErrorMessage(name);
    }

    public override bool IsValid(object? value)
    {
        Student? student = _context.Students.Where(x => x.Name == value.ToString()).FirstOrDefault();

        return student == null;
    }
}
