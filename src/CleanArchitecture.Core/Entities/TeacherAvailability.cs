namespace CleanArchitecture.Core.Entities
{
    public class TeacherAvailability
    {
        public int TeacherAvailabilityID { get; set; }
        public int TeacherId { get; set; }
        public int Day { get; set; }
        public int Period { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}