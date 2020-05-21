using System;

namespace Ankietnik
{
    public partial class MainOwner : System.Web.UI.Page
    {
        static string username;

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
                    username = Session["Name"].ToString();
                    var questList = QuestionService.GetQuestionnairesForOwner(username);
                    if (questList != null && questList.Count != 0)
                    {
                        HeaderSprawdźBrak.Visible = false;
                        HeaderSprawdź.Visible = true;
                        ListSprawdz.Visible = true;

                        ListSprawdz.DataSource = QuestionService.GetArrayListOfIds(questList);
                        ListSprawdz.DataBind();
                    } 
                    else
                    {
                        HeaderSprawdźBrak.Visible = true;
                        HeaderSprawdź.Visible = false;
                        ListSprawdz.Visible = false;
                    }
                }
            }
        }

        protected void ButtonSprawdz_Click(object sender, EventArgs e)
        {
            var selectedQuestId = ListSprawdz.SelectedValue;
            Response.Redirect($"CheckOwner.aspx?q={selectedQuestId}");
        }
    }
}