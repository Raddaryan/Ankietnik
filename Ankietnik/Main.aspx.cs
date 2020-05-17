using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null)
            {
                Response.Redirect($"Login.aspx");
            }
            else
            {
                String userName = "";
                userName = Session["Name"].ToString();
                var questionnairesList = QuestionService.GetPendingQuestionnairesForUser(userName);

                ListBoxWypelnij.DataSource = QuestionService.GetArrayListOfIds(questionnairesList);
                ListBoxWypelnij.DataBind();
            }

        }


        protected void ListBoxWypelnij_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}