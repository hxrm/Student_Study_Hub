<%@ Page Title="" Language="C#" MasterPageFile="~/Outside.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StudentStudyHub.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid" style="padding-block-start: 15px">
        <div class="row">
            <%--The Placement of the Login x,y in Container--%>
            <div class="col-md-8 mx-auto">
                <div class="card card border-dark">
                    <h1 style="padding: 15px; text-align: center">Study Hub Login        
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
                                            <asp:Label ID="saveMsg" runat="server" CssClass="text-danger" Text="Email or Password Invalid" Visible="false"></asp:Label>
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

                        <%--Email--%>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <div class="form-floating">
                                        <asp:TextBox ID="email" CssClass="form-control" runat="server" placeholder="student@email.co.za"></asp:TextBox>
                                        <label for="floatingInput">Email address</label>
                                        <asp:Label ID="emailError" CssClass="error-message invalid-feedback" runat="server" Text="Email Required" Visible="false"></asp:Label>
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
                                        <asp:TextBox ID="passWord" TextMode="Password" CssClass="form-control" runat="server" placeholder="password"></asp:TextBox>
                                        <label for="floatingInput">Password</label>
                                        <asp:Label ID="passWordError" CssClass="error-message invalid-feedback" runat="server" Text="Password Required" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />


                        <%--Buttons--%>
                        <div class="form-group">
                            <%--OnClick="submitButton_Click" --%> <%-- OnClick="registerButton_Click" --%>
                            <asp:Button ID="logInBtn" CssClass="form-control btn btn-primary btn-dark btn-block btn-lg" runat="server" Text="Login" OnClick="submitButton_Click" />
                        </div>

                    </div>
                    <br />
                    <br />
                    <a href="Register.aspx" class="text-center">
                        <p>Don't have an account? <strong>Register Here</strong>.</p>
                    </a>
                </div>
            </div>
        </div>
        <div style="margin-bottom: 80px;"></div>
    </div>

</asp:Content>

