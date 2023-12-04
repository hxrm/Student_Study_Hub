using StudentStudyHub.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace StudentStudyHub
{
    public partial class CurrentHours : System.Web.UI.Page
    {
        int selectedMod;
        int selectedWeek = 0;
        DBHelper dbHelper = new DBHelper();
        public DataTable moduleDataTable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                modDropDownList.DataSource = DBHelper.moduleList;
                modDropDownList.DataTextField = "ModuleCode";
                modDropDownList.DataValueField = "modID";
                modDropDownList.DataBind();


                weekDropDownList.DataSource = DBHelper.moduleList;
                // Populate the weekComboBox with week numbers.
                for (int i = 0; i <= DBHelper.weekSpan.Count; i++)
                {
                    weekDropDownList.Items.Add("Week " + (i + 1));
                }
                
            }
          /*  modDropDownList.DataSource = DBHelper.moduleList;
            modDropDownList.DataTextField = "ModuleCode";
            modDropDownList.DataValueField = "modID";
            modDropDownList.DataBind();


            weekDropDownList.DataSource = DBHelper.moduleList;
            // Populate the weekComboBox with week numbers.
            for (int i = 0; i <= DBHelper.weekSpan.Count; i++)
            {
                weekDropDownList.Items.Add("Week " + (i + 1));
            }
          
            */
        }

        protected void homeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void modDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            moduleDataTable.Rows.Clear();
            moduleGridView.DataSource = moduleDataTable;
            moduleGridView.DataBind();
            // selectedMod = Convert.ToInt32(modDropDownList.SelectedIndex);
            getMod();
            if (selectedWeek != 0)
            {
                ChangeView();
            }
        }
        protected void weekDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the moduleListView.
           // moduleGridView.DataSource = null;
            moduleGridView.DataBind();
            // Get the selected module index and week index from the combo boxes.
            //soen has for week 2 => find week 3...however the soen table has for week 2 an the study tracker saves 0
            selectedWeek = weekDropDownList.SelectedIndex;
            getMod();
            ChangeView();
        }
        public void getMod()
        {
            msgHrs2.Visible = false;

            if (!string.IsNullOrEmpty(modDropDownList.SelectedValue))
            {
                selectedMod = Convert.ToInt32(modDropDownList.SelectedValue);
                string modCode = modDropDownList.SelectedItem.Text;
                dbHelper.FindModule(modCode);

            }
        }
        private void ChangeView()
        {
            string modName = DBHelper.moduleList[selectedMod].ModuleName;
            int actHrs = 0;           
            int rHrs = DBHelper.moduleList[selectedMod].ExeStudyHrs;

            // Check if the selected module has study hours for the selected week.
            if (DBHelper.moduleList[selectedMod].studyTrack.ContainsKey(selectedWeek))
            {
                // Get the study hours for the selected module and week.
                actHrs = DBHelper.moduleList[selectedMod].studyTrack[selectedWeek].hoursStudied;
                rHrs = DBHelper.moduleList[selectedMod].studyTrack[selectedWeek].remainingHours;

            }
            moduleDataTable.Columns.Add("ModuleName", typeof(string));
            moduleDataTable.Columns.Add("ActStudyHrs", typeof(int));
            moduleDataTable.Columns.Add("RemainHrs", typeof(int));
          
            moduleDataTable.Rows.Add(modName,actHrs,rHrs);
           
            // Show or hide a message depending on whether there are study hours for the selected week.
            if (actHrs == 0)
            {
                msgHrs2.Text = "No hours saved for week, " + DBHelper.moduleList[selectedMod].ExeStudyHrs + " hours expected for module.";
                msgHrs2.Visible = true;
            }
            else if (actHrs > DBHelper.moduleList[selectedMod].ExeStudyHrs)
            {
                msgHrs2.Text = "Well done, you've studied " + (actHrs - DBHelper.moduleList[selectedMod].ExeStudyHrs) + " extra hour.";
                msgHrs2.Visible = true;
            }
            else
            {
                // msgHrs.Visibility = Visibility.Hidden;
            }
            /// msgHrs.Visibility = Visibility.Visible;
            // Add the item to the moduleListView.
            moduleGridView.DataSource = moduleDataTable;
            moduleGridView.DataBind();

        }
    }
}