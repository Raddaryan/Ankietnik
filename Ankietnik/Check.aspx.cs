using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    public partial class Check : System.Web.UI.Page
    {

        int qint;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null)
            {
                Response.Redirect($"Login.aspx");
            }
            else
            {
                var qstring = Request.QueryString["q"];
                qint = int.Parse(qstring);
                var userName = Session["Name"].ToString();
                var questions = QuestionService.VerifyResponse(qint, userName);
                rpt.DataSource = questions;
                rpt.DataBind();
            }
        }

        protected void GoBack_Clik(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}