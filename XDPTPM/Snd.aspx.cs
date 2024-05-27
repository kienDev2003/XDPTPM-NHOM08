using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM
{
    public partial class Snd : System.Web.UI.Page
    {
        private class list
        {
            public string bank_id { get; set; }
            string account_no { get; set; }
            string amount { get; set; }
            string DESCRIPTION { get; set; }
            string ACCOUNT_NAME { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            List<list> lists = new List<list>();
            //lists.Add(new list { bank_id = vtx=});
            //string bank_id = txtIdBank.Value;
            //string account_no = txtSTK.Value;
            //string amount = txtPrice.Value;
            //string DESCRIPTION = txtContentBank.Value;
            //string ACCOUNT_NAME = txtNameCTK.Value;

            //string htmlQr = $"https://img.vietqr.io/image/{bank_id}-{account_no}-print.png?amount={amount}&addInfo={DESCRIPTION}&accountName={ACCOUNT_NAME}";

            //Session["imageQR"] = htmlQr;
            //Session["imageQR1"] = htmlQr;

            //Response.Redirect("WebForm1.aspx");
        }
    }
}