<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddHours.aspx.cs" Inherits="StudentStudyHub.AddHours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="padding-block-start: 15px">
        <div class="row">
            <%--The Placement of the Register x,y in Container--%>
            <div class="col-md-12 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="row">
                                <div class="col">
                                    <h3>Record Studied Hours</h3>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <hr />
                                    <!-- Horizontal row (line) -->
                                </div>

                                <div class="row">
                                    <div class="col">

                                        <asp:Label ID="saveMsg" runat="server" CssClass="text-warning" Visible="false" Style="padding-left: 20px; font-size: 14px"></asp:Label>
                                        </>
                                    </div>
                                </div>
                                <div>Select Module</div>
                                <div class="input-container">
                                    <asp:DropDownList ID="modDropDownList" runat="server" CssClass="form-select" AutoPostBack="true">
                                        <asp:ListItem Text="Select Module" Value="" />
                                    </asp:DropDownList>
                                    <asp:Label ID="nameError" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
                                </div>


                                <div>Select Study Date</div>
                                <div class="input-container">
                                    <asp:TextBox ID="studyDate" CssClass="form-control" TextMode="Date" runat="server" placeholder="02-03-2023"></asp:TextBox>
                                    <asp:Label ID="dateError" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
                                </div>

                                <div>Enter Hours Studied</div>
                                <div class="input-container">
                                    <asp:TextBox ID="numHrs" CssClass="form-control" TextMode="Number" runat="server" placeholder="18"></asp:TextBox>
                                    <asp:Label ID="hoursError" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
                                </div>



                                <!-- Buttons -->
                                <div class="row" style="padding-block-start: 15px">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="saveButton" CssClass="form-control btn btn-primary btn-success btn-block btn-lg" runat="server" Text="Save Hours" OnClick="saveButton_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="homeButton" CssClass="form-control btn btn-primary btn-dark btn-block btn-lg" runat="server" Text="Home" OnClick="homeButton_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
