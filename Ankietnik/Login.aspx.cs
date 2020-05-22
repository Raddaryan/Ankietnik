using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    /// <summary>
    /// Strona logowania z walidacją pól i możliwością przekierowania do strony rejestracji.
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] != null)
            {
                Response.Redirect($"Main.aspx");
            }

            if (Session["Registered"] != null)
            {
                ShowMessage(Constants.RegistrationSuccessMsg, WarningType.Success, true);
                Session.Abandon();
            }
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
                Session["Name"] = UsernameTextBox.Text;

                if (AccountService.GetUser(UsernameTextBox.Text).Role == 0)
                {
                    Response.Redirect("Main.aspx");
                }
                else
                {
                    Response.Redirect("MainOwner.aspx");
                }
                
            }
            else
            {
                ShowMessage(operationResult.Message, WarningType.Warning, false);
            }

        }

        public void ShowMessage(string message, WarningType type, bool visibility)
        {
            Panel PanelMessage = FindControl("Message") as Panel;
            Label labelMessage = PanelMessage.FindControl("labelMessage") as Label;

            labelMessage.Text = message;
            PanelMessage.CssClass = string.Format("alert alert-{0} alert-dismissable", type.ToString().ToLower());
            PanelMessage.Attributes.Add("role", "alert");
            PanelMessage.Visible = visibility;
        }

    }

}