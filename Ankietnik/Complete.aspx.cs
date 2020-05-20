﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ankietnik
{
    public partial class Complete : System.Web.UI.Page
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
                var qstring = Request.QueryString["q"];
                qint = int.Parse(qstring);
                var questions = QuestionService.GetQuestions(qint);
                rpt.DataSource = questions;
                rpt.DataBind();
            }
            
        }

        protected void ButtonWyslijOdp_Clik(object sender, EventArgs e)
        {
            var userName = Session["Name"].ToString();
            var answerList = new List<Response>();
       
            if (rpt.Items.Count > 0)
            {
                
                foreach (RepeaterItem item in rpt.Items)
                {
                    var response = new Response();
                    response.QuestionId = int.Parse(((HiddenField)item.FindControl("hiddenId")).Value);
                    response.Content = Convert.ToBoolean(int.Parse(((RadioButtonList)item.FindControl("YesNo")).SelectedItem.Value));
                    answerList.Add(response);
                }
            }

            var operationResult = QuestionService.SubmitResponse(qint, answerList, userName, PasswordTextBox.Text);

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