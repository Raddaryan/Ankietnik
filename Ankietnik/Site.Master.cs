using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    /// <summary>
    /// MasterPage - element nagłówka z odnośnikiem do strony głównej oraz możliwością wylogowania.
    /// </summary>
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null)
            {
                Response.Redirect($"Login.aspx");
            } 
            else
            {
                lnkHome.NavigateUrl = AccountService.GetUser(Session["Name"].ToString()).Role == Constants.Roles[Constants.Role.Owner] ?
                                      "~/MainOwner.aspx" :
                                      "~/Main.aspx";
            }
        }

        protected void LogOutButton_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect($"Login.aspx");
        }
    }
}