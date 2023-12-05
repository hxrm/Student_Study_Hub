<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateModules.aspx.cs" Inherits="StudentStudyHub.CreateModules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid" style="padding-block-start: 15px">

        <div class="container-fluid">
            <div class="row">
                <div class="col-md-11 mx-auto">
                    <div class="card">
                        <div class="card-body">


                            <div class="row">
                                <div class="col">
                                    <h3>Create Module</h3>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <div class="form-group">
                                        <div class="form-label">
                                            <asp:Label ID="saveMsg" runat="server" CssClass="text-success" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col" style="padding-bottom: 10px">
                                    <hr />
                                    <!-- Horizontal row (line) -->
                                </div>
                            </div>
                            <!-- Input Sem Data-->
                            <div class="row" id="SemData">
                                <!-- Semester Start Date -->
                                <div class="col-md-6">
                                    <label>Semester Start Date</label>
                                    <div class="form-group">
                                        <div class="input-container">
                                            <asp:TextBox ID="startDate" CssClass="form-control" TextMode="Date" runat="server" placeholder="02-03-2023"></asp:TextBox>
                                            <asp:Label ID="dateError" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <!-- Weeks left in semester -->
                                <div class="col-md-6">
                                    <label>Weeks left in semester</label>
                                    <div class="form-group">
                                        <div class="input-container">
                                            <asp:TextBox ID="weeksLeft" CssClass="form-control" TextMode="Number" runat="server" placeholder="15"></asp:TextBox>
                                            <asp:Label ID="weeksError" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col" style="padding: 10px">
                                    <hr />
                                    <!-- Horizontal row (line) -->
                                </div>
                            </div>
                            <!-- Input Row 1 -->
                            <div class="row">
                                <!-- Module Name -->
                                <div class="col-md-6">
                                    <label>Module Name</label>
                                    <div class="form-group">
                                        <div class="input-container">
                                            <asp:TextBox ID="modName" CssClass="form-control" runat="server" placeholder="Programming 2B"></asp:TextBox>
                                            <asp:Label ID="nameError" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <!-- Module Code -->
                                <div class="col-md-6">
                                    <label>Module Code</label>
                                    <div class="form-group">
                                        <div class="input-container">
                                            <asp:TextBox ID="modCode" CssClass="form-control" runat="server" placeholder="PROG6212"></asp:TextBox>
                                            <asp:Label ID="codeError" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Input Row 2 -->
                            <div class="row">
                                <!-- Module Credits -->
                                <div class="col-md-6">
                                    <label>Module Credits</label>
                                    <div class="form-group">
                                        <div class="input-container">
                                            <asp:TextBox ID="modCredits" CssClass="form-control" TextMode="Number" runat="server" placeholder="18"></asp:TextBox>
                                            <asp:Label ID="creditError" CssClass="text-danger" runat="server" Text="TextBlock" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <!-- Module Weekly Class Hours -->
                                <div class="col-md-6">
                                    <label>Weekly Class Hours</label>
                                    <div class="form-group">
                                        <div class="input-container">
                                            <asp:TextBox ID="modClassHrs" CssClass="form-control" TextMode="Number" runat="server" placeholder="8"></asp:TextBox>
                                            <asp:Label ID="hoursError" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div style="margin-bottom: 30px;"></div>
                                <!-- Buttons -->
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <!-- OnClick="submitButton_Click" -->
                                            <asp:Button ID="createButton" CssClass="form-control btn btn-primary btn-success btn-block btn-lg" runat="server" Text="Create Module" OnClick="createButton_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <!-- OnClick="nextButton_Click" -->
                                            <asp:Button ID="nextButton" CssClass="form-control btn btn-primary btn-dark btn-block btn-lg" runat="server" Text="Next" OnClick="nextButton_Click" />
                                        </div>
                                        <div style="margin-bottom: 20px;"></div>
                                    </div>

                                </div>

                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>


        <div style="margin-bottom: 80px;"></div>
</asp:Content>
