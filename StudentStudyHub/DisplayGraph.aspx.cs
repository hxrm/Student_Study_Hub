using StudentStudyHub.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace StudentStudyHub
{
    public partial class DisplayGraph : System.Web.UI.Page
    {
        int selectedMod, selectedIndex;
        bool badStudent;
        DBHelper dbHelper = new DBHelper();
        public DataTable moduleDataTable = new DataTable();
        public List<StudyData> graphData = new List<StudyData>();
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
        protected void homeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void modDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            moduleDataTable.Rows.Clear();
           
            getMod(); 
          
        }
        public void getMod()
        {
            msgHrs2.Visible = false;

            if (!string.IsNullOrEmpty(modDropDownList.SelectedValue))
            {
                selectedMod = Convert.ToInt32(modDropDownList.SelectedValue);
                selectedIndex = Convert.ToInt32(modDropDownList.SelectedIndex);
                string modCode = modDropDownList.SelectedItem.Text;
                dbHelper.FindModule(modCode);

            }

            foreach (StudyData studyData in DBHelper.studyDataList)
            {
                if (studyData.ModID == selectedMod)
                {
                    graphData.Add(studyData);
                }

            }
            if (graphData.Count == 0)
            {
                badStudent = true;

            }
                            ChangeView();
                
        }
        private void ChangeView()
        {
            int goalHrs = DBHelper.moduleList[selectedIndex].ExeStudyHrs;
            int size = graphData.Count;         
            // Clear existing data in the chart
            Chart1.Series.Clear();
            
            // Create a new series for the chart

            Series series = new Series("StudyDataSeries");
            series.LegendText = "Actual hours of study";

            // Create a new series for the secondary Y-axis
            Series goal = new Series("GoalDataSeries");
            goal.LegendText = "Ideal study hours";

            if (!badStudent)
            {
                // Add data points to the series
                foreach (StudyData studyData in graphData)
                {
                    series.Points.AddXY(studyData.CurWeek, studyData.HoursWorked);

                }
            }
            else if (badStudent)
            {
                size = 3;
                for (int i = 0; i < size; i++)
                {
                    series.Points.AddXY(i, 0);
                }
                msgHrs2.Text = "No hours saved for module, " + DBHelper.moduleList[selectedIndex].ExeStudyHrs + " hours expected for module.";
                msgHrs2.Visible = true;
            }
            for (int i = 0; i < size; i++)
            {
                goal.Points.AddXY(i, goalHrs);
            }
            // Add the series to the chart
            Chart1.Series.Add(series);
            Chart1.Series.Add(goal);


            // Update the Legend
            Legend legend = Chart1.Legends.FindByName("Legend1");
            if (legend == null)
            {
                legend = new Legend("Legend1");
                Chart1.Legends.Add(legend);
            }
            Chart1.Series["StudyDataSeries"].Legend = "Legend1";
            Chart1.Series["GoalDataSeries"].Legend = "Legend1";
           

            Chart1.Series["StudyDataSeries"].ToolTip = "#VALY hours studied for module";
            Chart1.Series["GoalDataSeries"].ToolTip = "#VALY hours ideal for module";
           
        }
    }
}