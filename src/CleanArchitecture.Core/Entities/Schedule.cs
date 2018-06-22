using CleanArchitecture.Core.SharedKernel;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class Schedule : BaseEntity<int>
    {
        public Schedule()
        {
            this.ScheduleItems = new HashSet<ScheduleItem>();
        }

        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool IsValid { get; set; }
        public bool IsActive { get; set; }
        public decimal Fitness { get; set; }

        public virtual ICollection<ScheduleItem> ScheduleItems { get; set; }
    }
}