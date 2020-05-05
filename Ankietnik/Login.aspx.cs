using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                ShowMessage(operationResult.Message, WarningType.Danger);
            }
                
        }

        public enum WarningType
        {
            Success,
            Info,
            Warning,
            Danger
        }

        public void ShowMessage(string Message, WarningType type)
        {
            Panel PanelMessage = Master.FindControl("Message") as Panel;
            Label labelMessage = PanelMessage.FindControl("labelMessage") as Label;

            labelMessage.Text = Message;
            PanelMessage.CssClass = string.Format("alert alert-{0} alert-dismissable", type.ToString().ToLower());
            PanelMessage.Attributes.Add("role", "alert");
            PanelMessage.Visible = true;
        }



    }

}