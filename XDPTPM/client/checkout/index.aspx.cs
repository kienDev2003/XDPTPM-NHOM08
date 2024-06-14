using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM.client.checkout
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<checkOut> list = new List<checkOut>();
                list = (List<checkOut>)Session["checkOut"];
                setContent(list);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(\"Truy cập không hợp lệ\")</script>");
            }
        }

        private void setContent(List<checkOut> list)
        {
            string url_qr_bank = $"https://img.vietqr.io/image/{list[0].ID_Bank}-{list[0].STK}-{list[0].Template}?amount={list[0].total}&addInfo=Thanh_Toan_Hoa_Don_Ban_{list[0].Table_ID}&accountName={list[0].CTK}";
            img_qr_bank.Src = url_qr_bank;
            bank.Value = list[0].ID_Bank;
            nameAccount.Value = list[0].CTK;
            account_number.Value = list[0].STK;
            bank_content.Value = list[0].content.ToString();
            amount.Value = list[0].total;
        }

        [WebMethod]
        public static bool checkPayment()
        {
            Random r = new Random();

            if (r.Next(7, 9) == 8)
            {
                return true;
            }
            return false;
        }
    }
}