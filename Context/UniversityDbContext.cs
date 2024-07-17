using Microsoft.EntityFrameworkCore;
using Web_API_Rate_Limiting.Models;

namespace Web_API_Rate_Limiting.Context
{
    public class UniversityDbContext:DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourses>().HasKey(sc => new { sc.StudentId, sc.CourseId });
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentCourses> StudentCourses { get; set; }
    }
}
