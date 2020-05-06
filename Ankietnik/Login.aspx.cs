using System;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogInButton_Click(object sender, EventArgs e)
        {

            var operationResult = AccountService.Login(UsernameTextBox.Text, PasswordTextBox.Text);

            if (operationResult.Status == OperationStatus.Failed)
            {
                ShowMessage(operationResult.Message, WarningType.Danger, true);
            }

            else if (operationResult.Status == OperationStatus.Success)
            {
                ShowMessage(operationResult.Message, WarningType.Success, true);
                Response.Redirect("Main.aspx");
            }
            else
            {
                ShowMessage(operationResult.Message, WarningType.Warning, false);
            }

        }

        public void ShowMessage(string Message, WarningType type, bool Visibility)
        {
            Panel PanelMessage = FindControl("Message") as Panel;
            Label labelMessage = PanelMessage.FindControl("labelMessage") as Label;

            labelMessage.Text = Message;
            PanelMessage.CssClass = string.Format("alert alert-{0} alert-dismissable", type.ToString().ToLower());
            PanelMessage.Attributes.Add("role", "alert");
            if (Visibility == true)
            {
                PanelMessage.Visible = true;
            }
            else
            {
                PanelMessage.Visible = false;
            }
        }

    }

}