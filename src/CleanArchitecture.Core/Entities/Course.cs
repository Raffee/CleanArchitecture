using CleanArchitecture.Core.SharedKernel;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class Course : BaseEntity<int>
    {
        public Course()
        {
            this.CourseForGrades = new HashSet<CourseForGrade>();
        }

        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public int DifficultyLevelId { get; set; }

        public virtual ICollection<CourseForGrade> CourseForGrades { get; set; }
    }
}