using StudentStudyHub.Classes;
using StudyDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
//using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls.Primitives;
//using System.Windows.Media;

namespace StudentStudyHub.Classes
{/*select * from [dbo].[User] 
select * from [dbo].[MODULE] 
select * from [dbo].[ST_SEMESTER] 
select * from [dbo].[STUDY_TRACKER]

--DELETE FROM [dbo].[User]
--DELETE FROM [dbo].[MODULE]
--DELETE FROM [dbo].[ST_SEMESTER]
--DELETE FROM [dbo].[STUDY_TRACKER] */
    public class DBHelper
    {
        // Database path and connection string
        static string relativePath = "|DataDirectory|\\TimeAppDB.mdf";
        //string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={relativePath};Integrated Security=True";
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\StudentStudyHub\\StudentStudyHub\\App_Data\\StudyDB.mdf;Integrated Security=True";

        // VARIABLES
        static int userID;
        int selMod, modExpHrs, modIndex;
        DateTime modSDate;

        // DLL
        StudyD dLL = new StudyD(); // Create an instance of the StudyD class from the StudyDLL

        //HOLDER WEEKSPAN ARRAY FOR THE WEEK PERIODS 
        static public List<StudyData> studyDataList = new List<StudyData>();
        static public List<(DateTime WeekStart, DateTime WeekEnd)> weekSpan = new List<(DateTime WeekStart, DateTime WeekEnd)>();
        static public List<Module> moduleList = new List<Module>(); // List to store modules
        static public Semester stSemester = new Semester();
        static public bool hasSem = false;

        // Default constructor
        public DBHelper() { }

        // Parameterized constructor to initialize 
        public DBHelper(int inputUser)
        {
            userID = inputUser;
            FindModulesByUserID();
            FindSemesterByUserID();
            GetStudyData();
        }
        public List<StudyData> GetStudyData()
        {           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ModID, CurWeek, HoursWorked, RemainHours FROM STUDY_TRACKER " +
                               "WHERE ModID IN (SELECT ModID FROM MODULE WHERE UserID = @UserId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudyData studyData = new StudyData
                            {
                                ModID = Convert.ToInt32(reader["ModID"]),
                                CurWeek = Convert.ToInt32(reader["CurWeek"]),
                                HoursWorked = Convert.ToInt32(reader["HoursWorked"]),
                                //update to the found module exp hours
                                GoalHrs = 12
                            };

                            studyDataList.Add(studyData);
                        }
                    }
                }
            }

            return studyDataList;
        }

        // Method to find modules by user ID,Method to retrieve modules associated with the provided user ID
        public void FindModulesByUserID()
        {
            moduleList.Clear(); // Clear the module list before populating with new data

            //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM [MODULE] WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read()) // Iterate through each record for the provided user ID
                        {
                            Module module = new Module();
                            module.ModID = Convert.ToInt32(reader["ModID"]);
                            module.ModuleName = reader["ModName"].ToString();
                            module.ModuleCode = reader["ModCode"].ToString();
                            module.ModuleCredits = Convert.ToInt32(reader["Credits"]);
                            module.ModuleHrs = Convert.ToInt32(reader["ClassHrs"]);
                            module.ExeStudyHrs = Convert.ToInt32(reader["ExpHrs"]);
                            //    module.ActStudyHrs = Convert.ToInt32(reader["WorkedHrs"]);

                            if (!moduleList.Any(mod => mod.ModuleCode == module.ModuleCode))
                            {
                                moduleList.Add(module); // Add the retrieved module to the moduleList
                            }

                        }

                        reader.Close(); // Close the reader once done
                    }

                    connection.Close();
                }
            }
            // for each loop to iterate through each module 
            for (int i = 0; i < moduleList.Count; i++)
            {
                FindStudyTrackerByModID(moduleList[i].ModID);

            }
        }
        // Method to find study tracker by module ID,  find and retrieve study tracker information for a module
        public void FindStudyTrackerByModID(int selMod)
        {
            //  string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM [STUDY_TRACKER] WHERE ModID = @ModID";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@ModID", selMod);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read()) // Iterate through each record for the provided user ID
                        {
                            int curWeek = (int)reader["CurWeek"];
                            int hoursWorked = (int)reader["HoursWorked"];
                            int remainHours = (int)reader["RemainHours"];
                            for (int i = 0; i < moduleList.Count; i++)
                            {
                               int id = moduleList[i].ModID;
                              
                                if (selMod == id)
                                {
                                    moduleList[i].ActStudyHrs = remainHours;
                                    moduleList[i].StudyTracker(curWeek, hoursWorked, remainHours);
                                    break; // Assuming selMod is unique, you can break out of the loop once you've found the matching module.
                                }
                            }

                        }
                        reader.Close(); // Close the reader once done
                    }
                    connection.Close();
                }
            }
        }
        // Method to find the semester by user ID, retrieve the semester associated with the provided user ID
        public void FindSemesterByUserID()
        {
            //  string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM [ST_SEMESTER] WHERE ST_UserID = @UserID";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Create a new Semester object
                            Semester semester = new Semester
                            {
                                SemWeeks = (int)reader["TotalWeeks"],
                                SemStart = (DateTime)reader["StartDate"]

                            };

                            stSemester = semester;
                            hasSem = true;
                            StudyWeeks();
                        }
                        else if (!reader.Read())
                        {
                            // No semester found for the given UserID
                            stSemester = null;
                            hasSem = false;
                        }
                        connection.Close();
                    }
                }
            }
        }
        // Method to save a module's information into the database
        public void SaveModule(Module mod)
        {
            string m_Name = mod.ModuleName;
            string m_Code = mod.ModuleCode;
            int m_Credits = mod.ModuleCredits;
            int m_ClassHrs = mod.ModuleHrs;
            int m_ExpHrs = 0;
            mod.SetExpHours(dLL.StudyCalculator(m_Credits, m_ClassHrs, stSemester.SemWeeks));
            m_ExpHrs = modExpHrs;

            //  string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";
            m_ExpHrs = mod.ExeStudyHrs;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO [MODULE] (UserId, ModName, ModCode, Credits, ClassHrs, ExpHrs) " +
                                    "VALUES (@UserId, @ModName, @ModCode, @Credits, @ClassHrs, @ExpHrs)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userID);
                    command.Parameters.AddWithValue("@ModName", m_Name);
                    command.Parameters.AddWithValue("@ModCode", m_Code);
                    command.Parameters.AddWithValue("@Credits", m_Credits);
                    command.Parameters.AddWithValue("@ClassHrs", m_ClassHrs);
                    command.Parameters.AddWithValue("@ExpHrs", m_ExpHrs);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        if (!moduleList.Any(modu => modu.ModuleCode == mod.ModuleCode))
                        {
                            moduleList.Add(mod); // Add the retrieved module to the moduleList
                        }
                    }
                }

                connection.Close();
            }
        }
        // Method to find a module by its code, retrieved from databases 
        public void FindModule(string modCode)
        {
            modIndex = 0;

            //  string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM [MODULE] WHERE ModCode = @ModCode";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@ModCode", modCode);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Check if the record exists in the database.
                        {
                            string pulledMod = reader["ModCode"].ToString();

                            if (modCode == pulledMod) // Check if the entered module code matches the stored code.
                            {
                                selMod = Convert.ToInt32(reader["ModID"]); // This assigns the value of modID to selMod
                                modExpHrs = Convert.ToInt32(reader["ExpHrs"]);
                            }

                            reader.Close(); // Close the reader once done
                        }

                        connection.Close();
                    }
                }
            }
            for (int i = 0; i < moduleList.Count; i++)
            {
                if (moduleList[i].ModID == selMod)
                {
                    modIndex = i;
                }
            }
        }
        // Method to save a semester's information into database
        public void SaveSemester(Semester sem)
        {
            int weeksLeft = sem.SemWeeks;
            DateTime startDate = sem.SemStart.Date;
            int totalWeeks = sem.weekSpan.Count;
            stSemester = sem;
            StudyWeeks();

            //  string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO [ST_SEMESTER] (ST_UserID, StartDate, WeeksLeft, TotalWeeks) " +
                                    "VALUES (@ST_UserID, @StartDate, @WeeksLeft, @TotalWeeks)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@ST_UserID", userID);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@WeeksLeft", weeksLeft);
                    command.Parameters.AddWithValue("@TotalWeeks", totalWeeks);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        hasSem = true;
                    }
                    else
                    {
                        hasSem = false;
                    }
                }

                connection.Close();
            }
        }
        // Method to calculate and set the weekly periods
        public List<(DateTime WeekStart, DateTime WeekEnd)> StudyWeeks()
        {

            DateTime startDate = stSemester.startDate;
            modSDate = startDate;
            for (int week = 0; week < stSemester.SemWeeks; week++)
            {
                DateTime weekStart = modSDate;
                DateTime weekEnd = modSDate.AddDays(6); // Saturday is the end of the week
                weekSpan.Add((weekStart, weekEnd));

                // Move to the next week
                modSDate = modSDate.AddDays(7);
            }

            return weekSpan;
        }

        // Private method to find the week for a given date
        public int FindWeek(DateTime inputDate, out string errorMessage)
        {
            DateTime endDate = stSemester.SemStart.AddDays(stSemester.SemWeeks * 7);
            errorMessage = "";
            //FIND THE CODE'S WEEKSLEFT / TOTALWEEKS
            if (inputDate > endDate || inputDate < stSemester.SemStart)
            {
                //  DateTime endDate = stSemester.SemStart.AddDays(stSemester.SemWeeks * 6);
                errorMessage = "Select Date Between " + stSemester.SemStart.ToShortDateString() + " and " + endDate.ToShortDateString();
                return -1;
            }

            for (int week = 0; week < stSemester.SemWeeks; week++)
            {
                if (inputDate >= weekSpan[week].WeekStart && inputDate <= weekSpan[week].WeekEnd)
                {
                    errorMessage = "";
                    return week + 1;

                }

            }
            // Return -1 if the current date is not within any week
            return -1;
        }
        // Method to update study tracker,  update the study tracker with new study hours
        public void UpdateTracker(DateTime studyDay, int hrsWorked, int inputWeek)
        { // string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\TimeApplication\\TimeApplication\\TimeAppDB.mdf;Integrated Security=True";
            // Flag to determine if the database record needs an update
            bool update = false;
            // Calculate remaining hours after study
            int week = inputWeek;
            // Calculating remaining hours
            int remainHrs = modExpHrs - hrsWorked;
            // To track the new hours for a week
            int newHrs = 0;

            // Check if the module already has tracked hours for the current week
            if (moduleList[modIndex].studyTrack.ContainsKey(week))
            {// Module's hours for the week already exist, indicating an update is required
                update = true;
            }
            // Check if remaining hours are negative after study
            if (remainHrs < 0)
            {   // Reset remaining hours to zero after studying             
                remainHrs = 0;
                // Reset the new hours if update flag is set
                if (update)
                {
                    newHrs = modExpHrs;
                }
            }
            // Insert new study data for the week if it does not exist
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (!update)
                {
                    // Insert new study data for the week if it does not exist
                    string insertQuery = "INSERT INTO [STUDY_TRACKER] (ModID, CurWeek, HoursWorked, RemainHours) " +
                                                "VALUES (@ModID, @CurWeek, @HoursWorked, @RemainHours)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters for the command
                        insertCommand.Parameters.AddWithValue("@HoursWorked", hrsWorked);
                        insertCommand.Parameters.AddWithValue("@RemainHours", remainHrs);
                        insertCommand.Parameters.AddWithValue("@ModID", selMod);
                        insertCommand.Parameters.AddWithValue("@CurWeek", week);

                        // Execute the update or insert command
                        insertCommand.ExecuteNonQuery();
                    }
                    moduleList[modIndex].StudyTracker(week, hrsWorked, remainHrs);

                }
                else if (update)
                {
                    // Check if the current module and week already have hours saved
                    string selectQuery = "SELECT * FROM [STUDY_TRACKER] WHERE ModID = @ModID AND CurWeek = @CurWeek";

                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@ModID", selMod);
                        selectCommand.Parameters.AddWithValue("@CurWeek", week);

                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // If data exists, update the hours
                                newHrs = (int)reader["HoursWorked"] + hrsWorked;
                                remainHrs = (int)reader["RemainHours"] - hrsWorked;
                                if (remainHrs < 0)
                                {// if remaining hrs are negative make 0 
                                    remainHrs = 0;
                                }
                                reader.Close(); // Close the reader before proceeding
                            }
                        }
                        // Update or insert the study hours and remaining hours for the module's week
                        string updateOrInsertQuery = "UPDATE [STUDY_TRACKER] SET HoursWorked = @NewHours, RemainHours = @RemainHrs WHERE ModID = @ModID AND CurWeek = @CurWeek";


                        using (SqlCommand updateOrInsertCommand = new SqlCommand(updateOrInsertQuery, connection))
                        {
                            // Add parameters for the command
                            updateOrInsertCommand.Parameters.AddWithValue("@NewHours", newHrs);
                            updateOrInsertCommand.Parameters.AddWithValue("@RemainHrs", remainHrs);
                            updateOrInsertCommand.Parameters.AddWithValue("@ModID", selMod);
                            updateOrInsertCommand.Parameters.AddWithValue("@CurWeek", week);

                            // Execute the update or insert command
                            updateOrInsertCommand.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                    // Update local study tracker for the module with the updated information
                    moduleList[modIndex].StudyTracker(week, newHrs, remainHrs);
                }
            }
        }

    }// end class 
    public class StudyData
    {
        public int ModID { get; set; }
        public int CurWeek { get; set; }
        public int HoursWorked { get; set; }
        public int GoalHrs{ get; set; }
    }

}

