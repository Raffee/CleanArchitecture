using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.SharedKernel;
using GAF;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public abstract class Rule : BaseEntity<Guid>, IMustHaveSchool
    {
        #region ENUMS

        public enum RuleTypes
        {
            TimeBased,
            CountBased,
            FixedDayPeriod
        }

        public enum CriteriaOperators
        {
            Exactly,
            AtLeast,
            AtMost
        }

        public enum CountTypes
        {
            CountConsecutive,
            CountPerDay
        }

        #endregion

        public RuleTypes RuleType { get; protected set; }
        public int RuleObjectType { get; set; }
        public int RuleObjectId { get; set; }
        public int? Teacher { get; set; }
        public int? Course { get; set; }
        public int? Department { get; set; }  // Department and Subject should be mutually exclusive. A rule applies either to a Department or a Subject
        public int? ClassLevel { get; set; } // Class Level and Class should be mutually exclusive. A rule applies either to a class or a class level
        public int? Class { get; set; }
        public int Priority { get; set; }
        public decimal FitnessCoefficient { get; set; }
        public int SchoolId { get; set; }

        public abstract decimal EvaluateFitness(ScheduleChromosome chromosome);
    }
}
