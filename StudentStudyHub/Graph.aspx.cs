using StudentStudyHub.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;


namespace StudentStudyHub
{
    public partial class Graph : System.Web.UI.Page
    {
       public List<StudyData> graphData = new List<StudyData>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call a method to bind data to the chart
                BindChart();
            }
        }

        private void BindChart()
        {
            graphData = DBHelper.studyDataList;
          
            // Clear existing data in the chart
            Chart1.Series.Clear();

            // Create a new series for the chart
            Series series = new Series("StudyDataSeries");
            series.ChartType = SeriesChartType.Line;

            Series goal = new Series("StudyDataSeries");
            goal.ChartType = SeriesChartType.Line;

            // Add data points to the series
            foreach (StudyData studyData in graphData)
            {
                series.Points.AddXY(studyData.HoursWorked, studyData.CurWeek);

            }
            // Add the series to the chart
            Chart1.Series.Add(series);
          
            Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            // Configure the Chart control for a line graph
            Chart1.Series["StudyDataSeries"].ChartType = SeriesChartType.Line;

            // Set some additional properties for better visualization (you can adjust these as needed)
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = true;
         


            Legend legend = Chart1.Legends.FindByName("Legend1");

            if (legend == null)
            {
                // Legend with the name "Legend1" does not exist, so create a new one
                legend = new Legend("Legend1");
                Chart1.Legends.Add(legend);
            }
            Chart1.Series["StudyDataSeries"].Legend = "Legend1";
            Chart1.Series["StudyDataSeries"].ToolTip = "#VALY hours";
        }
    }
}