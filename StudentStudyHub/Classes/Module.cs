using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentStudyHub.Classes
{

    public class Module
    {
        // VARIABLES
        public int modID;
        // Variables to store module-related information
        public string modCode;
        public string modName;
        public int modCredits;
        public int classHrs;

        // Variables to track study hours
        public int expectedStudyHrs = 0;
        public int actualStudyHrs = 0;

        // Dictionary to track study hours per week (week -> actual study hours)
        // public Dictionary<int, int> studyTrack = new Dictionary<int, int>();
        public Dictionary<int, (int hoursStudied, int remainingHours)> studyTrack = new Dictionary<int, (int, int)>();



        // Default constructor
        public Module() { }

        // Parameterized constructor to initialize module properties
        public Module(string name, string code, int credits, int classHours)
        {
            this.modCode = code;
            this.modName = name;
            this.modCredits = credits;
            this.classHrs = classHours;
        }

        // Method to track study hours for a specific week
        public void StudyTracker(int week, int workedHrs, int remainHrs)
        {
            if (studyTrack.ContainsKey(week))
            {
                studyTrack[week] = (workedHrs, remainHrs);
            }
            else
            {
                // If `week` doesn't exist in the dictionary, add a new entry.
                studyTrack.Add(week, (workedHrs, remainHrs));
            }

            // Update the actualStudyHrs property
            actualStudyHrs = workedHrs;
        }
        public void SetExpHours(int hrs)
        {
            expectedStudyHrs = hrs;
        }

        // Getter and setter properties for module-related information
        public int ModID { get => modID; set => modID = value; }
        public string ModuleCode { get => modCode; set => modCode = value; }
        public string ModuleName { get => modName; set => modName = value; }
        public int ModuleCredits { get => modCredits; set => modCredits = value; }
        public int ModuleHrs { get => classHrs; set => classHrs = value; }

        // Getter and setter properties for expected and actual study hours
        public int ExeStudyHrs { get => expectedStudyHrs; set => expectedStudyHrs = value; }
        public int ActStudyHrs { get => actualStudyHrs; set => actualStudyHrs = value; }
    }
}
