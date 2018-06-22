using CleanArchitecture.Core.SharedKernel;
using GAF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class CountBasedRule : Rule
    {
        public int? Day { get; set; }
        public int CriteriaValue { get; set; }
        public CountTypes CountType { get; set; }
        public CriteriaOperators CountCriteria { get; set; }
        public CriteriaOperators? OccuranceType { get; set; }
        public int OccuranceCount { get; set; }

        public CountBasedRule()
        {
            this.RuleType = RuleTypes.CountBased;
        }

        public override decimal EvaluateFitness(ScheduleChromosome chromosome)
        {
            var days = new List<int>();

            if (this.OccuranceType.HasValue)
            {
                for (var i = 0; i < SchoolSystemConstants.DAYS_PER_WEEK; i++)
                    days.Add(i);
            }
            else if (this.Day.HasValue)
            {
                days.Add(this.Day.Value);
            }
            var weeklyCountActual = 0;
            var violationSeverity = 0;

            foreach (var day in days)
            {
                var classIds = new List<int>();
                if (this.Class.HasValue)
                    classIds.Add(this.Class.Value);
                else if (this.ClassLevel.HasValue)
                    classIds.AddRange(new List<int>()); // Get the classes in this level

                foreach (var classId in classIds)
                {
                    var coursesForThisDay = chromosome.GetCourseInstancesForDayAndClass(classId, day);

                    if (this.CountType == CountTypes.CountPerDay)
                    {
                        if (this.Course.HasValue)
                            coursesForThisDay = coursesForThisDay.Where(ci => ci.CourseForGrade.CourseId.HasValue && ci.CourseForGrade.CourseId.Value == this.Course.Value).ToList();
                        else if (this.Teacher.HasValue)
                            coursesForThisDay = coursesForThisDay.Where(ci => ci.CourseForGrade.TeacherId.HasValue && ci.CourseForGrade.TeacherId.Value == this.Teacher.Value).ToList();

                        weeklyCountActual = IncrementWeeklyCountIfCriteriaApplies(weeklyCountActual, coursesForThisDay.Count);
                    }
                    else
                    {
                        var currentCourseId = 0; //Since the rule can apply to a teacher or a course.
                        var currentTeacherId = 0;
                        var consecutiveCountActual = 0;

                        foreach (var ci in coursesForThisDay)
                        {
                            if (this.Teacher.HasValue)
                                currentTeacherId = this.Teacher.Value;
                            if (this.Course.HasValue)
                                currentCourseId = this.Course.Value;

                            if ((currentCourseId == 0 || ci.CourseForGrade.CourseId == currentCourseId) && (currentTeacherId == 0 || ci.CourseForGrade.TeacherId == currentTeacherId))
                                consecutiveCountActual++;
                            else
                            {
                                if (consecutiveCountActual > 1)
                                {
                                    weeklyCountActual = IncrementWeeklyCountIfCriteriaApplies(weeklyCountActual, consecutiveCountActual);
                                }
                                consecutiveCountActual = 0;
                            }
                        }
                    }
                }
            }

            if (this.OccuranceType.Value == CriteriaOperators.AtLeast)
            {
                if (this.OccuranceCount > weeklyCountActual)
                    violationSeverity++;
            }
            else if (this.OccuranceType == CriteriaOperators.AtMost)
            {
                if (this.OccuranceCount < weeklyCountActual)
                    violationSeverity++;
            }
            else if (this.OccuranceType == CriteriaOperators.Exactly)
            {
                if (this.OccuranceCount != weeklyCountActual)
                    violationSeverity++;
            }

            return violationSeverity * this.FitnessCoefficient;
        }

        private int IncrementWeeklyCountIfCriteriaApplies(int weeklyCountActual, int consecutiveCountActual)
        {
            switch (this.CountCriteria)
            {
                //This means that there were consecutive courses before the current course instance. Therefore we need to check the rule violations here.
                case CriteriaOperators.AtLeast:
                    if (this.CriteriaValue <= consecutiveCountActual)
                        weeklyCountActual++;
                    break;
                case CriteriaOperators.AtMost:
                    if (this.CriteriaValue >= consecutiveCountActual)
                        weeklyCountActual++;
                    break;
                case CriteriaOperators.Exactly:
                    if (this.CriteriaValue == consecutiveCountActual)
                        weeklyCountActual++;
                    break;
            }

            return weeklyCountActual;
        }
    }
}
