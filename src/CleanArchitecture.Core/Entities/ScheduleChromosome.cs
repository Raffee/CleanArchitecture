using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanArchitecture.Core.SharedKernel;
using GAF;

namespace CleanArchitecture.Core.Entities
{
    public class ScheduleChromosome : Chromosome
    {
        public List<CourseInstance> GetCourseInstancesForDayAndClass(int classId, int day)
        {
            var courses = ConvertChromosomeToList();
            courses = courses.Where(ci => ci.Day == day && ci.CourseForGrade.GradeId == classId).ToList();

            return courses;
        }

        public List<CourseInstance> ConvertChromosomeToList()
        {
            //We can use AutoMapper for this
            var courses = new List<CourseInstance>();
            foreach (var gene in this.Genes)
                courses.Add((CourseInstance)gene.ObjectValue);

            return courses;
        }

        public CourseInstance[,,] ConvertChromosomeToArray()
        {
            var ciArray = new CourseInstance[SchoolSystemConstants.CLASSES_TOTAL, SchoolSystemConstants.DAYS_PER_WEEK, SchoolSystemConstants.PERIODS_PER_DAY];

            int cIndex = 0, dIndex = 0, pIndex = 0;

            foreach (var gene in this.Genes)
            {
                ciArray[cIndex, dIndex, pIndex] = gene.ObjectValue as CourseInstance;

                #region SETTING THE CORRECT INDECES

                if (pIndex < SchoolSystemConstants.PERIODS_PER_DAY - 1)
                    pIndex++;
                else
                {
                    pIndex = 0;

                    if (dIndex < SchoolSystemConstants.DAYS_PER_WEEK - 1)
                        dIndex++;
                    else
                    {
                        dIndex = 0;

                        if (cIndex < SchoolSystemConstants.CLASSES_TOTAL - 1)
                            cIndex++;
                        else
                            throw new IndexOutOfRangeException();
                    }
                }

                #endregion
            }

            return ciArray;
        }
    }
}
