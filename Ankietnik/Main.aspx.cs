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
                    var msg = Request.QueryString["msg"];
                    if (msg != null && msg == "successSubmission")
                    {
                        ShowMessage(Constants.ResponseSubmittedMsg, WarningType.Success, true);
                    }

                    var userName = Session["Name"].ToString();
                    var pendingList = QuestionService.GetPendingQuestionnairesForUser(userName);
                    var completedList = QuestionService.GetCompletedQuestionnairesForUser(userName);

                    if (pendingList == null)
                    {
                        ListWypelnij.Visible = false;
                        ButtonWypelnij.Enabled = false;
                        ButtonWypelnij.Visible = false;
                        HeaderWypelnij.Visible = false;
                    }
                    else
                    {
                        HeaderWypelnijBrak.Visible = false;
                    }

                    if (completedList == null)
                    {
                        HeaderSprawdź.Visible = false;
                        ListSprawdz.Visible = false;
                        ButtonSprawdz.Enabled = false;
                        ButtonSprawdz.Visible = false;
                    }
                    else
                    {
                        HeaderSprawdźBrak.Visible = false;
                    }


                    ListWypelnij.DataSource = QuestionService.GetArrayListOfIds(pendingList);
                    ListWypelnij.DataBind();
                    ListSprawdz.DataSource = QuestionService.GetArrayListOfIds(completedList);
                    ListSprawdz.DataBind();
                }
                else
                {
                    ShowMessage(string.Empty, WarningType.Success, false);
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
            Response.Redirect($"Check.aspx?q={selectedQuestId}");
        }

        public void ShowMessage(string message, WarningType type, bool visibility)
        {
            Panel PanelMessage = HelperService.FindControlRecursive(Page, "Message") as Panel;
            Label labelMessage = PanelMessage.FindControl("labelMessage") as Label;

            labelMessage.Text = message;
            PanelMessage.CssClass = string.Format("alert alert-{0} alert-dismissable", type.ToString().ToLower());
            PanelMessage.Attributes.Add("role", "alert");
            PanelMessage.Visible = visibility;
        }

    }
}