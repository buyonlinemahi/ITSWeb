using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace ITSWebApp.WebForms.Reports
{
    public partial class InitialAssessmentQAReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Request.QueryString["CaseID"].ToString()!=string.Empty)
                {
                ReportParameter CaseID = new ReportParameter();
                CaseID.Name = "CaseID";
                CaseID.Values.Add(Request.QueryString["CaseID"].ToString());
                rptInitialAssessmentQa.ProcessingMode = ProcessingMode.Remote;
                rptInitialAssessmentQa.ServerReport.ReportServerUrl = new System.Uri(ConfigurationManager.AppSettings["ReportServerUrl"].ToString().Trim());
                rptInitialAssessmentQa.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportPath"].ToString().Trim();
                rptInitialAssessmentQa.ServerReport.SetParameters(CaseID);
                rptInitialAssessmentQa.ShowToolBar = true;
                rptInitialAssessmentQa.ShowPrintButton = false;
                rptInitialAssessmentQa.ShowParameterPrompts = false;
                rptInitialAssessmentQa.ServerReport.Refresh();

                }
            }
        }
    }
}