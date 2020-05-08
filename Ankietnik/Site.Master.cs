using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null)
            {
                Response.Redirect($"Login.aspx");
            }
        }

        protected void LogOutButton_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect($"Login.aspx");
        }
    }
}