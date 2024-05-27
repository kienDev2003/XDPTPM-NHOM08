using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string qr = Session["imageQR"].ToString();

                bank.Attributes.Add("src", qr);

                Session.Remove("imageQR");


                //try
                //{
                //    qr = Session["imageQR"].ToString();
                //}
                //catch (Exception ex)
                //{
                //    Response.Redirect(Session["imageQR1"].ToString());
                //}

            }
            catch (Exception ex)
            {

            }


        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("Snd.aspx");
        }
    }
}