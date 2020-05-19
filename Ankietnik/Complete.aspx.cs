using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    public partial class Complete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null)
            {
                Response.Redirect($"Login.aspx");
            }
            else
            {
                var qstring = Request.QueryString["q"];
                int qint = int.Parse(qstring);
                var questions = QuestionService.GetQuestions(qint);
                rpt.DataSource = questions;
                rpt.DataBind();
            }
            
        }

        protected void ButtonWyslijOdp_Clik(object sender, EventArgs e)
        {

        }
    }
}