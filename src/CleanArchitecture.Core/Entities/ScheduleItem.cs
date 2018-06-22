using CleanArchitecture.Core.SharedKernel;

namespace CleanArchitecture.Core.Entities
{
    public class ScheduleItem : BaseEntity<int>
    {
        public int ScheduleId { get; set; }
        public int CourseForGradeId { get; set; }
        public int Day { get; set; }
        public int Period { get; set; }

        public virtual CourseForGrade CourseForGrade { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}