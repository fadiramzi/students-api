namespace StudentsManagerMW.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        //public ICollection<Student> Students { get; set; }
    }
}
