namespace StudentManagement.Context;

public class StudentContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }
    public StudentContext(DbContextOptions<StudentContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure many-to-many relationship between Student and Subject
        modelBuilder.Entity<StudentSubject>()
            .HasKey(ss => new { ss.StudentId, ss.SubjectId });

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Subjects)
            .WithMany(s => s.Students)
            .UsingEntity<StudentSubject>(
                j => j.HasOne(ss => ss.Subject)
                      .WithMany(s => s.StudentSubjects)
                      .HasForeignKey(ss => ss.SubjectId),
                i => i.HasOne(ss => ss.Student)
                      .WithMany(s => s.StudentSubjects)
                      .HasForeignKey(ss => ss.StudentId)
                );


        /*modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Student)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.StudentId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Subject)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);*/ // Disable cascade delete

        // Configure one-to-many relationship between Teacher and Subject
        modelBuilder.Entity<Subject>()
            .HasOne(s => s.Teacher)
            .WithMany(t => t.Subjects)
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

        // Configure one-to-many relationship between Department and Student
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Department)
            .WithMany(d => d.Students)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

        // Configure one-to-many relationship between Department and Teacher
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Department)
            .WithMany(d => d.Teachers)
            .HasForeignKey(t => t.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

        // Configure unique constraints and indices if necessary
        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Email)
            .IsUnique();

        modelBuilder.Entity<Teacher>()
            .HasIndex(t => t.Email)
            .IsUnique();
    }
}

