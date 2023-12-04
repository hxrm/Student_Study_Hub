using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using StudyDLL; // Import a custom DLL named "StudyDLL"

namespace StudentStudyHub.Classes
{
    public class Semester
    {
        // VARIABLES
        public int weeksLeft; // Number of weeks left in the semester
        public DateTime startDate; // Start date of the semester
        public List<(DateTime WeekStart, DateTime WeekEnd)> weekSpan = new List<(DateTime WeekStart, DateTime WeekEnd)>(); // List to store weekly periods
        public List<Module> moduleList = new List<Module>(); // List to store modules

        // DLL
        StudyD dLL = new StudyD(); // Create an instance of the StudyD class from the StudyDLL

        // CONSTRUCTORS
        public Semester() { } // Default constructor
        public Semester(List<Module> modules, int weeks, DateTime date)
        {
            // Parameterized constructor
            this.moduleList = modules; // Initialize the moduleList with the provided modules
            this.weeksLeft = weeks; // Initialize weeksLeft with the provided weeks
            this.startDate = date; // Initialize startDate with the provided date

            SetSelfStudy(); // Calculate and set self-study hours for modules
            StudyWeeks(); // Calculate and set the weekly periods
        }


        // Method to set expected self-study hours for all modules
        public void SetSelfStudy()
        {
            for (int i = 0; i < moduleList.Count; i++)
            {
                this.moduleList[i].expectedStudyHrs = dLL.StudyCalculator(this.moduleList[i].ModuleCredits, this.moduleList[i].ModuleHrs, SemWeeks);
            }
        }

        // Private method to find the week for a given date
        private int FindWeek(DateTime date)
        {
            for (int week = 0; week < weeksLeft; week++)
            {
                if (date >= weekSpan[week].WeekStart && date <= weekSpan[week].WeekEnd)
                {
                    return week;
                }
            }
            // Return -1 if the current date is not within any week
            return -1;
        }


        // Method to calculate and set the weekly periods
        public List<(DateTime WeekStart, DateTime WeekEnd)> StudyWeeks()
        {
            // Calculate the weekly periods for the remaining weeks in the semester
            DateTime currentDate = startDate;
            for (int week = 0; week < weeksLeft; week++)
            {
                DateTime weekStart = currentDate;
                DateTime weekEnd = currentDate.AddDays(6); // Saturday is the end of the week
                weekSpan.Add((weekStart, weekEnd));

                // Move to the next week
                currentDate = currentDate.AddDays(7);
            }

            return weekSpan; // Return the list of weekly periods
        }

        // GETTERS AND SETTERS
        public int SemWeeks { get => weeksLeft; set => weeksLeft = value; }
        public DateTime SemStart { get => startDate; set => startDate = value; }


    }// end class
}
