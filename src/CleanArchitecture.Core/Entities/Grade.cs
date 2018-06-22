using CleanArchitecture.Core.SharedKernel;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class Grade : BaseEntity<int>
    {
        public Grade()
        {
            this.CourseForGrades = new HashSet<CourseForGrade>();
        }

        public string Name { get; set; }
        public int NumericOrder { get; set; }
        public int GradeLevelId { get; set; }

        public virtual ICollection<CourseForGrade> CourseForGrades { get; set; }
    }
}