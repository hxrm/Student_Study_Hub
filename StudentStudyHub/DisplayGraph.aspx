<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayGraph.aspx.cs" Inherits="StudentStudyHub.DisplayGraph" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card" style="padding-block-start: 10px">
        <div class="card-body mx-auto">
            <div class="row">
                <div class="col">
                    <h3>Weekly Study Progress</h3>
                </div>
            </div>

            <div class="row">
                <div class="col">
                    <hr>
                </div>
            </div>
            <div class="row">
                <div class="col">

                    <asp:Label ID="msgHrs2" runat="server" CssClass="text-warning" Visible="false"></asp:Label>
                    </>
                </div>
            </div>
            <!-- DropDown -->
            <div class="row">
                <div class="col mx-auto">
                    <div>Select Module</div>
                    <div class="input-container">
                        <asp:DropDownList ID="modDropDownList" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="modDropDownList_SelectedIndexChanged">
                            <asp:ListItem Text="Select Module" Value="" />
                        </asp:DropDownList>
                        <asp:Label ID="nameError" CssClass="text-danger" runat="server" Text="TextBlock" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col">
                    <hr>
                </div>
            </div>
            <!-- Graph -->
            <div class="card border-dark">
                <center>
                    <asp:Chart ID="Chart1" runat="server" Width="800px" Height="600px" BackColor="Indigo" BackGradientStyle="LeftRight" BackSecondaryColor="#1b0d33" Palette="SeaGreen" PaletteCustomColors="OliveDrab" RightToLeft="Yes">
                        <Series>
                            <asp:Series Name="StudyDataSeries" LegendText="Actual hours of study"></asp:Series>
                            <asp:Series Name="GoalDataSeries" LegendText="Ideal study hours"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="Transparent" ShadowColor="64, 0, 64" BackColor="Lavender">
                                <AxisX Title="Study Week" TitleFont="Sans Serif, 13pt, style=Bold" TitleForeColor="PaleGreen">
                                    <MajorGrid Enabled="False" />
                                </AxisX>

                                <AxisY Title="Hours Worked" TitleFont="Sans Serif, 13pt, style=Bold" TextOrientation="Horizontal" TitleAlignment="Far" TitleForeColor="PaleGreen">
                                    <MajorGrid Enabled="False" />
                                </AxisY>
                                <AxisY2 Title="Goal">
                                    <MajorGrid Enabled="False" />
                                </AxisY2>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Name="Legend1"></asp:Legend>
                        </Legends>
                        <BorderSkin BorderColor="Transparent" PageColor="DarkSlateBlue" />
                    </asp:Chart>
                </center>
            </div>

            <!-- Buttons -->
            <div class="row" style="padding-block-start: 15px">
                <div class="col">
                    <div class="form-group">
                        <asp:Button ID="homeButton" CssClass="form-control btn btn-primary btn-success btn-block btn-lg" runat="server" Text="Home" OnClick="homeButton_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
