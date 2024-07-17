namespace Web_API_Rate_Limiting.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int DepartmentId { get; set;}
        public Department Department { get; set; }

        public List<StudentCourses> StudentCourses { get; set; } = new List<StudentCourses>();
    }
}
