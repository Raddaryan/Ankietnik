using System;
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
                if (!IsPostBack)
                {
                    ButtonWypelnij.Enabled = false;
                    ButtonSprawdz.Enabled = false;
                    ButtonWypelnij.Visible = false;
                    ButtonSprawdz.Visible = false;

                    var userName = Session["Name"].ToString();
                    var pendingList = QuestionService.GetPendingQuestionnairesForUser(userName);
                    var completedList = QuestionService.GetCompletedQuestionnairesForUser(userName);

                    ListWypelnij.DataSource = QuestionService.GetArrayListOfIds(pendingList);
                    ListWypelnij.DataBind();
                    ListSprawdz.DataSource = QuestionService.GetArrayListOfIds(completedList);
                    ListSprawdz.DataBind();
                }
            }
        }


        protected void ListWypelnij_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonWypelnij.Enabled = true;
            ButtonWypelnij.Visible = true;
        }

        protected void ListSprawdz_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonSprawdz.Enabled = true;
            ButtonSprawdz.Visible = true;
        }

        protected void ButtonWypelnij_Click(object sender, EventArgs e)
        {
            var selectedQuestId = ListWypelnij.SelectedValue;
            Response.Redirect($"Complete.aspx?q={selectedQuestId}");
        }

        protected void ButtonSprawdz_Click(object sender, EventArgs e)
        {
            var selectedQuestId = ListSprawdz.SelectedValue;
        }
        
    }
}