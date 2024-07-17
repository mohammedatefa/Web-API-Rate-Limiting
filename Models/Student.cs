namespace Web_API_Rate_Limiting.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string  Grade { get; set; }
        public string  Status { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<StudentCourses> StudentCourses { get; set; } = new List<StudentCourses>();

    }
}
