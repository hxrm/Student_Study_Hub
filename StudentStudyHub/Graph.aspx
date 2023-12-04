<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Graph.aspx.cs" Inherits="StudentStudyHub.Graph" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Study Data Graph</title>
 <style>
     /* Add any additional styles as needed */
     .chart-container {
         width: 80%;
         margin: auto;
     }
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="chart-container">
        <asp:Chart ID="Chart1" runat="server" Width="1000px" Height="500px">
            <Series>
                <asp:Series Name="StudyDataSeries" ChartType="Line"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisX Title="Hours Worked">
                        <MajorGrid Enabled="False" />
                    </AxisX>
                    <AxisY Title="Study Week">
                        <MajorGrid Enabled="False" />
                    </AxisY>
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1"></asp:Legend>
            </Legends>
        </asp:Chart>
    </div>
</asp:Content>

