using StudentStudyHub.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentStudyHub
{
    public partial class AddHours : System.Web.UI.Page
    {
        DBHelper dbHelper = new DBHelper();
        int week = 0;
        bool validDate;
        bool validHours;
        int selectedMod;

        // Declare variables to store error messages
        string hrsErrorMessage, dateErrorMessage;

        // Create an instance of the Validation class
        public Validation v = new Validation();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                modDropDownList.DataSource = DBHelper.moduleList;
                modDropDownList.DataTextField = "ModuleCode";
                modDropDownList.DataValueField = "modID";
                modDropDownList.DataBind();
            }
           
        }
        protected void saveButton_Click(object sender, EventArgs e)
        {
            DateTime d;
            getMod();
           
            //Validate Date input 
            validDate = v.TryDate(studyDate.Text, out dateErrorMessage, out d);
            dateError.Text = dateErrorMessage;
            dateError.Visible = true;
            //Validate Date falls within week  
            week = dbHelper.FindWeek(d,out dateErrorMessage);
            dateError.Text = dateErrorMessage;
            dateError.Visible = true;

            // Validate the input for hours and store the error message
            validHours = v.TryReceiveNumber(numHrs.Text, out hrsErrorMessage);
            hoursError.Text = hrsErrorMessage;
            hoursError.Visible = true;
            
            int wk = week;
            
            //find week ???? db method 

            // Check if the input for hours is valid
            if (validHours && validDate)
            {
                // Parse the entered hours as an integer
                int stHrs = int.Parse(numHrs.Text);
                dbHelper.UpdateTracker(d, stHrs, wk);

                // Display a message indicating that hours are saved
                saveMsg.Text = DBHelper.moduleList[selectedMod].ModuleName + "'s Hours Saved ";
                saveMsg.Visible = true;

            }
        }

        protected void homeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void modDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            getMod();

        }
        public void getMod ()
        { 
            saveMsg.Visible = false;

            if (!string.IsNullOrEmpty(modDropDownList.SelectedValue))
            {// IS TAKEING THE MODID NOT THEINDEX
                selectedMod = Convert.ToInt32(modDropDownList.SelectedValue);
                string modCode = modDropDownList.SelectedItem.Text;
                dbHelper.FindModule(modCode);
               
            }
        }

        public void Rest()
        {
            numHrs.Text = "";
            studyDate.Text = "";
            hoursError.Visible = false;
            dateError.Visible = false;
            saveMsg.Visible = false;
        }
    }
}