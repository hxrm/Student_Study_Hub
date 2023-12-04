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

            // Create a new series for the secondary Y-axis
            Series goal = new Series("GoalDataSeries");
            goal.ChartType = SeriesChartType.Line;

            // Add data points to the series
            foreach (StudyData studyData in graphData)
            {
                series.Points.AddXY(studyData.CurWeek, studyData.HoursWorked);

            }
            for (int i = 0; i <( graphData.Count +4); i++)
            {
               
                // Add corresponding data points for the secondary Y-axis
                goal.Points.AddXY(i, 12);
            }
            // Add the series to the chart
            Chart1.Series.Add(series);
            Chart1.Series.Add(goal);

            // Associate the secondary Y-axis with the series
            Chart1.Series["GoalDataSeries"].YAxisType = AxisType.Secondary;

            // Configure the Chart control for a line graph
            Chart1.Series["StudyDataSeries"].ChartType = SeriesChartType.Line;
            Chart1.Series["GoalDataSeries"].ChartType = SeriesChartType.Line;
            
            // Update the Legend
            Legend legend = Chart1.Legends.FindByName("Legend1");
            if (legend == null)
            {
                legend = new Legend("Legend1");
                Chart1.Legends.Add(legend);
            }
            Chart1.Series["StudyDataSeries"].Legend = "Legend1";
            Chart1.Series["GoalDataSeries"].Legend = "Legend1";

            Chart1.Series["StudyDataSeries"].ToolTip = "#VALY hours";
            Chart1.Series["GoalDataSeries"].ToolTip = "#VALY Goal";
        }
    }
}