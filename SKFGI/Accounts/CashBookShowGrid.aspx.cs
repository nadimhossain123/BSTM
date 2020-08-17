using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Accounts;
using System.Drawing;
using System.Text;

namespace SKFGI.Accounts
{
    public partial class CashBookShowGrid : System.Web.UI.Page
    {
        clsGeneralFunctions gf = new clsGeneralFunctions();
        char chr = Convert.ToChar(130);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (int.Parse(Session["CompanyId"].ToString()) == 2)
                    {
                        imgHeader.ImageUrl = "~/Images/DiplomaHeader.JPG";
                        
                    }
                    else if (int.Parse(Session["CompanyId"].ToString()) == 1)
                    {
                        imgHeader.ImageUrl = "~/Images/ReportHeader1.png";
                    }
                    else
                    {
                        imgHeader.ImageUrl = "~/Images/SEDCO.jpg";
                    }


                    lblReportHeader.Text = Session[clsGlobalVariable.sesReportTitle].ToString();

                    if (Session[clsGlobalVariable.sesReportPageHeader] != null || Session[clsGlobalVariable.sesReportPageHeader].ToString() != "")
                        PlaceHolder2.Controls.Add(new LiteralControl(Session[clsGlobalVariable.sesReportPageHeader].ToString()));
                    if (Session[clsGlobalVariable.sesReportPageFooter] != null || Session[clsGlobalVariable.sesReportPageFooter].ToString() != "")
                        PlaceHolder3.Controls.Add(new LiteralControl(Session[clsGlobalVariable.sesReportPageFooter].ToString()));

                    if (Session[clsGlobalVariable.sesReportGrid] != null)
                    {
                        GridView gv = (GridView)Session[clsGlobalVariable.sesReportGrid];
                        if (gv != null)
                        {
                            PlaceHolder1.Controls.Add(gv);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //
                }
            }

        }
    }
}