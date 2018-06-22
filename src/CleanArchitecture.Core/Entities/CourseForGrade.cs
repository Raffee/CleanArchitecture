using CleanArchitecture.Core.SharedKernel;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class CourseForGrade : BaseEntity<int>
    {
        public CourseForGrade()
        {
            this.CourseInstances = new HashSet<CourseInstance>();
            this.ScheduleItems = new HashSet<ScheduleItem>();
        }

        public int? CourseId { get; set; }
        public int GradeId { get; set; }
        public int? TeacherId { get; set; }
        public int PeriodsPerWeek { get; set; }

        public virtual Course Course { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<CourseInstance> CourseInstances { get; set; }
        public virtual ICollection<ScheduleItem> ScheduleItems { get; set; }
    }
}