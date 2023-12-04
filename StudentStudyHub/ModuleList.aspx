<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuleList.aspx.cs" Inherits="StudentStudyHub.ModuleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card"  style="padding-block-start: 10px">
        <div class="card-body col-md-11 mx-auto"> 
            <div class="row">
                <div class="col">                   
                     <h3>Module List</h3>                   
                </div>
            </div>

            <div class="row">
                <div class="col">
                    <hr>
                </div>
            </div>

            <div class="row">
                <div class="col">
                    <asp:GridView class="table table-striped table-bordered" ID="moduleGridView" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="modID" >
                        <Columns>
                            <asp:BoundField DataField="ModuleName" HeaderText="Module Name" SortExpression="ModuleName" />
                            <asp:BoundField DataField="ExeStudyHrs" HeaderText="Weekly Study Hours" SortExpression="ExeStudyHrs" />
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
