using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM.admin.quanly.view
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["loginToken"].ToString() == "" || Session["loginToken"].ToString() == null)
                {
                    Response.Redirect("../../login/index.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../../login/index.aspx");
            }
        }
    }
}