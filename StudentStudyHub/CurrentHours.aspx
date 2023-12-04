<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrentHours.aspx.cs" Inherits="StudentStudyHub.CurrentHours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card" style="padding-block-start: 10px">

        <div class="card-body col-md-11 mx-auto">
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
                    <center>
                        <asp:Label ID="msgHrs2" runat="server" CssClass="text-waring" Visible="false"></asp:Label>
                    </center>
                </div>
            </div>

            <!-- DropDown -->
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <div>Select Module</div>
                    <div class="input-container">
                        <asp:DropDownList ID="modDropDownList" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="modDropDownList_SelectedIndexChanged">
                            <asp:ListItem Text="Select Module" Value="" />
                        </asp:DropDownList>
                        <asp:Label ID="nameError" CssClass="text-danger" runat="server" Text="TextBlock" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-md-6 mx-auto">
                    <div>Select Study Week </div>
                    <div class="input-container">
                        <asp:DropDownList ID="weekDropDownList" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="weekDropDownList_SelectedIndexChanged">
                            <asp:ListItem Text="Select Week" Value="" />
                        </asp:DropDownList>
                        <asp:Label ID="weeksError" CssClass="text-danger" runat="server" Text="TextBlock" Visible="false"></asp:Label>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col">
                    <hr>
                </div>
            </div>
            <!-- List -->
            <div class="card card border-dark">
                <div class="col">
                    <asp:GridView class="table table-striped table-bordered" ID="moduleGridView" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ModuleName" HeaderText="Module Name" SortExpression="ModuleName" />
                            <asp:BoundField DataField="ActStudyHrs" HeaderText="Hours Studied" SortExpression="ActStudyHrs" />
                            <asp:BoundField DataField="RemainHrs" HeaderText="Hours Remaining" SortExpression="RemainHrs" />
                        </Columns>
                        <HeaderStyle CssClass="headerStyle" />
                        <RowStyle CssClass="rowStyle" />
                        <SelectedRowStyle CssClass="selectedStyle" />
                    </asp:GridView>
                </div>
            </div>
            <!-- Buttons -->
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Button ID="homeButton" CssClass="form-control btn btn-primary btn-success btn-block btn-lg" runat="server" Text="Home" OnClick="homeButton_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
