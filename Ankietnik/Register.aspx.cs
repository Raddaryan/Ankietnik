using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            AccountService.Register(NewUsernameTextBox.Text, NewPasswordTextBox.Text, RetypedTextBox.Text, int.Parse(GroupTextBox.Text));
        }
    }
}