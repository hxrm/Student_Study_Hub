using StudentStudyHub.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentStudyHub
{
    public partial class ModuleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // Set the data source for 'moduleGridView'
            moduleGridView.DataSource = DBHelper.moduleList;
            moduleGridView.DataBind();
        }

        protected void homeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}