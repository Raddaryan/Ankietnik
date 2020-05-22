using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    /// <summary>
    /// Strona wyświetlające listę odpowiedzi na ankietę z wynikami, ilość osób, które odpowiedziały na ankietę oraz listę tych użytkowników, którzy jeszcze nie odpowiedzieli. 
    /// Odpowiedzi zostaną pobrane z bazy danych i wyświetlone dopiero po weryfikajcji hasłem i rozszyfrowaniu podpisu.
    /// </summary>
    public partial class CheckOwner : System.Web.UI.Page
    {
        static int qint;

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
                    var qstring = Request.QueryString["q"];
                    qint = int.Parse(qstring);
                    var completed = QuestionService.GetNumberOfCompletedForQuest(qint);
                    if (completed == 0)
                    {
                        ((Label)HelperService.FindControlRecursive(Page, "HeaderNieMaOdp")).Visible = true;
                        ((Label)HelperService.FindControlRecursive(Page, "HeaderLiczbaOdpOwner")).Visible = false;
                        ((Label)HelperService.FindControlRecursive(Page, "lblLiczbaWypelnionych")).Visible = false;
                        ((Label)HelperService.FindControlRecursive(Page, "HeaderWynikiOwner")).Visible = false;
                        rpt.Visible = false;
                    }
                    else
                    {
                        ((Label)HelperService.FindControlRecursive(Page, "lblLiczbaWypelnionych")).Text = completed.ToString();
                        ((Label)HelperService.FindControlRecursive(Page, "HeaderNieMaOdp")).Visible = false;

                        var scores = QuestionService.GetScoresForQuest(qint);
                        rpt.DataSource = scores;
                        rpt.DataBind();
                    }

                    var pendingUsers = QuestionService.GetListOfUsersPendingForQuest(qint);
                    if (pendingUsers == string.Empty)
                    {
                        ((Label)HelperService.FindControlRecursive(Page, "HeaderWszyscyWypelnili")).Visible = true;
                    }
                    else
                    {
                        ((Label)HelperService.FindControlRecursive(Page, "KtoNieWypelnil")).Text = pendingUsers;
                        ((Label)HelperService.FindControlRecursive(Page, "HeaderWszyscyWypelnili")).Visible = false;
                    }
                }
                else
                {
                    ShowMessage(string.Empty, WarningType.Info, false);
                }
            }
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