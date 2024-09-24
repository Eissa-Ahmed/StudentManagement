namespace StudentManagement.ViewModel
{
    public class StudentSubjectsViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public List<int> SelectedSubjectIds { get; set; } = new List<int>(); // To hold selected subjects
        public List<Subject> AvailableSubjects { get; set; } = new List<Subject>(); // To hold all available subjects
    }
}
