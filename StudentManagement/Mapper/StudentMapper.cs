using AutoMapper;
using StudentManagement.ViewModel;

namespace StudentManagement.Mapper;

public class StudentMapper : Profile
{
    public StudentMapper()
    {
        CreateMap<CreateStudentVM, Student>()
            .ForMember(dest => dest.StudentSubjects, opt => opt.MapFrom<StudentMapper_Create_Resolver>());
    }
}


public class StudentMapper_Create_Resolver : IValueResolver<CreateStudentVM, Student, List<StudentSubject>>
{
    public List<StudentSubject> Resolve(CreateStudentVM source, Student destination, List<StudentSubject> destMember, ResolutionContext context)
    {
        List<StudentSubject> result = new List<StudentSubject>();
        foreach (var item in source.SubjectsIds)
        {
            result.Add(new StudentSubject { StudentId = destination.Id, SubjectId = item });
        }
        return result;
    }
}