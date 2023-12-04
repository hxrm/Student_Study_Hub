<%@ Page Title="" Language="C#" MasterPageFile="~/Outside.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="StudentStudyHub.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container-fluid" style="padding-block-start: 15px">
        <div class="row">
            <%--The Placement of the Register x,y in Container--%>
            <div class="col-md-8 mx-auto">
                <div class="card card border-dark">
                    <h1 style="padding: 15px; text-align: center">Study Hub Register        
                    </h1>
                    <div class="card-body">
                        <div class="row">

                            <div class="row">
                                <center>
                                    <img width="200" src="/Style/Logo1.png" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <div class="form-label">
                                        <center>
                                            <asp:Label ID="saveMsg" runat="server" CssClass="text-success" Visible="false"></asp:Label>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <hr />
                                <%--horizontail row (line)--%>
                            </div>
                        </div>
                        <%--Name--%>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <div class="form-floating">
                                        <asp:TextBox ID="fName" CssClass="form-control" runat="server" placeholder="name"></asp:TextBox>
                                        <label for="floatingInput">Firstname</label>
                                        <asp:Label ID="fNameError" CssClass=" invalid-feedback" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <%--Surname--%>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <div class="form-floating">
                                        <asp:TextBox ID="sName" CssClass="form-control" runat="server" placeholder="surname"></asp:TextBox>
                                        <label for="floatingInput">Surname</label>
                                        <asp:Label ID="sNameError" CssClass="invalid-feedback" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <%--Email--%>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <div class="form-floating">
                                        <asp:TextBox ID="email" CssClass="form-control" runat="server" placeholder="student@email.co.za"></asp:TextBox>
                                        <label for="floatingInput">Email</label>
                                        <asp:Label ID="emailError" CssClass=" invalid-feedback" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <%--Password--%>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <div class="form-floating">
                                        <asp:TextBox ID="passWord" CssClass="form-control" TextMode="Password" runat="server" placeholder="password"></asp:TextBox>
                                        <label for="floatingInput">Password</label>
                                        <asp:Label ID="passWordError" CssClass="invalid-feedback" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <%--Confirm Password--%>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <div class="form-floating">
                                        <asp:TextBox ID="conP" CssClass="form-control" TextMode="Password" runat="server" placeholder="confirm password"></asp:TextBox>
                                        <label for="floatingInput">Confirm Password</label>
                                        <asp:Label ID="conPError" CssClass="invalid-feedback" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <%--Buttons--%>
                        <div class="form-group">
                            <asp:Button ID="createUser" CssClass="form-control btn btn-primary btn-success btn-block btn-lg" runat="server" Text="Register" OnClick="createUser_Click" />
                        </div>
                    </div>

                    <br />
                    <br />
                    <a href="Login.aspx" class="text-center">
                        <p>Already have an account? <strong>Login Here</strong>.</p>
                    </a>
                </div>
            </div>
        </div>
        <div style="margin-bottom: 80px;"></div>
    </div>
</asp:Content>
