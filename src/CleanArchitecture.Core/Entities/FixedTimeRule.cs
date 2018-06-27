using CleanArchitecture.Core.SharedKernel;
using GAF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class FixedTimeRule : Rule
    {
        public int Day { get; set; }
        public int Period { get; set; }
        public new int Class { get; set; }
        public new int Teacher { get; set; }
        public new int Course { get; set; }

        public FixedTimeRule()
        {
            this.RuleType = RuleTypes.FixedDayPeriod;
        }

        public override decimal EvaluateFitness(ScheduleChromosome chromosome)
        {
            var coursesForThisDay = chromosome.GetCourseInstancesForDayAndClass(Class, Day);
            var courseAtSpecifiedPeriod = coursesForThisDay.SingleOrDefault(c => c.Period == Period);

            var violationSeverity = 1;

            if (courseAtSpecifiedPeriod != null && courseAtSpecifiedPeriod.CourseForGrade.TeacherId == Teacher &&
                courseAtSpecifiedPeriod.CourseForGrade.CourseId == Course)
                violationSeverity = 0;

            return violationSeverity * this.FitnessCoefficient;
        }
    }
}
