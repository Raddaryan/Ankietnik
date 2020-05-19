using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    public partial class Complete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var test = Request.QueryString["test"];
        }

        protected void ButtonWyslijOdp_Clik(object sender, EventArgs e)
        {

        }
    }
}