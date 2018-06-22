using CleanArchitecture.Core.SharedKernel;
using GAF;

namespace CleanArchitecture.Core.Entities
{
    public class CourseInstance : Gene
    {
        public int? Day { get; set; }
        public int? Period { get; set; }
        public int CourseInstanceID { get; set; }
        public int? CourseForGradeId { get; set; }
        public int? OccuranceCount { get; set; }

        public virtual CourseForGrade CourseForGrade { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as CourseInstance;
            return Equals(item);
        }

        protected bool Equals(CourseInstance other)
        {
            return CourseForGradeId.Equals(other.CourseForGradeId) && OccuranceCount.Equals(other.OccuranceCount);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (CourseForGradeId != null ? CourseForGradeId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ OccuranceCount.GetHashCode();
                return hashCode;
            }
        }
    }
}