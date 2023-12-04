using StudentStudyHub.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace StudentStudyHub
{
    public partial class CreateModules : System.Web.UI.Page
    {
        //Declare DBHelper Object 
        DBHelper dbHelper = new DBHelper();
        // Declare instance variables
        string nameErrorMessage, codeErrorMessage, creditErrorMessage, hrsErrorMessage;
        // Instantiate objects and initialize variables
        public Validation v = new Validation(); // Validation object
        public Module mod = new Module(); // Module object
        public List<Module> moduleList = new List<Module>(); // List to store modules
        public Semester sem = new Semester(); // Semester object
        public bool createSem = false; // Boolean flag
        private Menu menu = new Menu(); // Menu object

        protected void Page_Load(object sender, EventArgs e)
        {
           EnableSem();
        }
        
        private void EnableSem()
        {
            if(DBHelper.hasSem == true)
            {
                createSem = false;
                startDate.Enabled = false;
                weeksLeft.Enabled = false;
                dateError.Visible = false;
                weeksError.Visible = false;

            }
            else
            {
                createSem = true;
            }
            
        }

        protected void createButton_Click(object sender, EventArgs e)
        {
           
            CreateSem();
            CreateMod();
        }
        public void CreateSem()
        {
            if (createSem == true)
            {
                string errorMessage = "";
                DateTime date;

                // Validate and receive the number of weeks left input
                bool validInput = v.TryReceiveNumber(weeksLeft.Text, out errorMessage);
                weeksError.Text = errorMessage;
                weeksError.Visible = true;

                // Validate and receive the start date input
                bool validDate = v.TryDate(startDate.Text, out errorMessage, out date);
                dateError.Text = errorMessage;
                dateError.Visible = true;

                if (validInput && validDate)
                {
                    sem = new Semester(moduleList, int.Parse(weeksLeft.Text), date);
                    // pass to database helper 
                    dbHelper.SaveSemester(sem);
                }
            }
        }

        public void CreateMod()
        {
            Reset1(); // Call Reset1 method to hide error messages and reset some elements
            bool validName = v.TryReceiveString(modName.Text, out nameErrorMessage);
            nameError.Text = nameErrorMessage;
            nameError.Visible = true;
            bool validCode = v.TryReceiveModuleCode(modCode.Text, out codeErrorMessage);
            codeError.Text = codeErrorMessage;
            codeError.Visible = true;
            bool validCredits = v.TryReceiveNumber(modCredits.Text, out creditErrorMessage);
            creditError.Text = creditErrorMessage;
            creditError.Visible = true;
            bool validHrs = v.TryReceiveNumber(modClassHrs.Text, out hrsErrorMessage);
            hoursError.Text = hrsErrorMessage;
            hoursError.Visible = true;

            if (validName && validCode && validCredits && validHrs)
            {
                // Add a new module to moduleList
              //  moduleList.Add(new Module { ModuleName = modName.Text, ModuleCode = modCode.Text, ModuleCredits = int.Parse(modCredits.Text), ModuleHrs = int.Parse(modClassHrs.Text) });
                mod = new Module(modName.Text, modCode.Text, int.Parse(modCredits.Text), int.Parse(modClassHrs.Text));
                saveMsg.Text = modCode.Text + " Created";
                saveMsg.Visible = true;
                nextButton.Enabled = true;
                // Call Reset method to clear textboxes and hide error messages
                Reset();
                // DBHelper method call to save module  to database 
                dbHelper.SaveModule(mod);
                DBHelper.stSemester.moduleList.Add(mod);
                

            }
        }
        protected void nextButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
        // Reset method to hide error messages and clear textboxes
        public void Reset()
        {
            modName.Text = string.Empty;
            modCode.Text = string.Empty;
            modCredits.Text = string.Empty;
            modClassHrs.Text = string.Empty;
            nameError.Visible = false;
            codeError.Visible = false;
            creditError.Visible = false;
            hoursError.Visible = false;
        }

        // Reset1 method to hide error messages
        public void Reset1()
        {
            nameError.Visible = false;
            saveMsg.Visible = false;
            codeError.Visible = false;
            creditError.Visible = false;
            hoursError.Visible = false;
        }

    }
}