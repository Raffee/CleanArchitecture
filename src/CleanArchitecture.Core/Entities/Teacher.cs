using CleanArchitecture.Core.SharedKernel;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class Teacher : BaseEntity<int>
    {
        public Teacher()
        {
            this.CourseForGrades = new HashSet<CourseForGrade>();
            this.TeacherAvailabilities = new HashSet<TeacherAvailability>();
        }

        public int TeacherID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CourseForGrade> CourseForGrades { get; set; }
        public virtual ICollection<TeacherAvailability> TeacherAvailabilities { get; set; }
    }
}