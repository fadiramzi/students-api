using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagerMW.Models
{
    // Student.cs
    [Keyless]
    public class StudentInput
    {



        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")] public DateTime BirthDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
    // Department.cs
    
}
