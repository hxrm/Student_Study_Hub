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

            // Add data points to the series
            foreach (StudyData studyData in graphData)
            {
                series.Points.AddXY(studyData.CurWeek, studyData.HoursWorked);
            }

            // Add the series to the chart
            Chart1.Series.Add(series);

            // Set chart properties as needed
            Chart1.ChartAreas[0].AxisX.Title = "Week";
            Chart1.ChartAreas[0].AxisY.Title = "Hours Worked";
            Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            // Configure the Chart control for a line graph
          //  Chart1.Series["Series1"].ChartType = SeriesChartType.Line;
          //  Chart1.Series["IdealSeries"].ChartType = SeriesChartType.Line;

            // Set some additional properties for better visualization (you can adjust these as needed)
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Week";
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Hours";

            // Optionally, you can add legends and tooltips
            Chart1.Legends.Add(new Legend("Legend1"));
           // Chart1.Series["Series1"].Legend = "Legend1";
            //Chart1.Series["IdealSeries"].Legend = "Legend1";
          //  Chart1.Series["Series1"].ToolTip = "#VALY hours";
           // Chart1.Series["IdealSeries"].ToolTip = "Ideal: #VALY hours";
        }
    }
}