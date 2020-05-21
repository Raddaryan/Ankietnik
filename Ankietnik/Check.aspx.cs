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
                if (!IsPostBack)
                {
                    var qstring = Request.QueryString["q"];
                    qint = int.Parse(qstring);
                } 
                else
                {
                    ShowMessage(string.Empty, WarningType.Info, false);
                }
            }
        }

        protected void GoBack_Clik(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }

        protected void ButtonSprawdz_Click(object sender, EventArgs e)
        {
            var result = QuestionService.GetAnswers(qint, Session["Name"].ToString(), PasswordTextBox.Text);

            if (result.Status == OperationStatus.Failed)
            {
                ShowMessage(result.Message, WarningType.Danger, true);
            }
            else
            {
                rpt.DataSource = result.Payload;
                rpt.DataBind();
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