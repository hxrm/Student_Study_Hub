<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="StudentStudyHub.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-bottom: 50px;"></div>
    <section>
        <div class="container">
            <div class="card border-dark" style="padding: 20px">
                <h6 class="text-white">Welcome</h6>
                <hr />
                <p class="text-justify text-white"><small>Study Progression:</small></p>
            </div>

            <%--Row 1--%>
            <div class="row">
                <div class="col-12" style="padding-block: 10px">
                    <h3 class="text-white">Study Hub Functions</h3>
                    <hr />
                </div>
            </div>
            <%--Row 2 : 1/3 coloumns ( 12 by 3: col-md-4 defines breakpoint to shift) --%>
            <div class="card border-primary" style="padding: 25px">
                <div class="row">
                    <div class="col-md-3">
                        <center>
                            <a href="CreateModules.aspx" style="text-decoration: none">
                                <img width="110" src="/Style/library.png" />
                                <h5 class="text-white" style="padding-top: 10px">Create New Module</h5>
                            </a>
                            <p class="text-justify text-white"><small>add new module to your module list for current semester</small></p>
                        </center>

                    </div>
                    <%--2/3 coloumns  --%>
                    <div class="col-md-3">
                        <center>
                            <a href="AddHours.aspx" style="text-decoration: none">
                                <img width="120" src="/Style/ebook.png" />
                                <h5 class="text-white">Record Study Hours</h5>
                            </a>
                            <p class="text-justify text-white "><small>Record hours studied for a module</small></p>

                        </center>
                    </div>
                    <%--3/3 coloumns  --%>
                    <div class="col-md-3">
                        <center>
                            <a href="CurrentHours.aspx" style="text-decoration: none">
                                <img width="120" src="/Style/trophy.png" />
                                <h5 class="text-white">Study Progress</h5>
                            </a>
                            <p class="text-justify text-white"><small>View of study progression and what's left</small></p>

                        </center>
                    </div>
                    <div class="col-md-3">
                        <center>
                            <a href="DisplayGraph.aspx" style="text-decoration: none">
                                <img width="120" src="/Style/Graph.png" />
                                <h5 class="text-white">Study Progress Graph</h5>
                            </a>
                            <p class="text-justify text-white"><small>View of study progression graph</small></p>

                        </center>
                    </div>
                </div>
            </div>
        </div>

    </section>
    <div style="margin-bottom: 50px;"></div>
</asp:Content>
