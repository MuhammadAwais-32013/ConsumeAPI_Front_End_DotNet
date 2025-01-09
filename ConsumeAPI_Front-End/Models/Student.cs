namespace ConsumeAPI_Front_End.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Semester { get; set; }
        public required float Cgpa { get; set; }
        public required string shift { get; set; }
    }
}
