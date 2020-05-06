using System;
using System.Threading;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            var operationResult = AccountService.Register(NewUsernameTextBox.Text, NewPasswordTextBox.Text, RetypedTextBox.Text, int.Parse(GroupTextBox.Text));

            if (operationResult.Status == OperationStatus.Failed)
            {
                ShowMessage(operationResult.Message, WarningType.Danger, true);
            }
            else if (operationResult.Status == OperationStatus.Success)
            {
                ShowMessage(operationResult.Message, WarningType.Success, true);
                Response.Redirect($"Login.aspx");
            }
            else{
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