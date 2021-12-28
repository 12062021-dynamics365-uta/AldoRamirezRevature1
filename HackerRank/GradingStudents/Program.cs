using System;
using System.Collections.Generic;

namespace GradingStudents
{
    class Program
    {
        public static List<int> gradingStudents(List<int> grades)
        {
            for (int i = 0; i < grades.Count; i++)
            {
                int grade = grades[i];
                if (grade >= 38)
                {
                    int diff = 5 - (grade % 5);
                    if (diff < 3)
                        grades[i] = grade + diff;
                }
            }
            return grades;
        }

        public static void Main(String[] args)
        {

        }
    }
}
