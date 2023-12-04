<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Graph.aspx.cs" Inherits="StudentStudyHub.Graph" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:Chart ID="Chart1" runat="server" Width="452px" Palette="Excel">
        <series>
            <asp:Series Name="Series1" ChartType="Line">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
    
</asp:Content>
