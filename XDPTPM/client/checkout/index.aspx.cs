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
        static List<order_dish> listOrder;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<checkOut> list = new List<checkOut>();
                listOrder = new List<order_dish>();
                list = (List<checkOut>)Session["checkOut"];
                listOrder = (List<order_dish>)Session["OrderList"];
                setContent(list);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(\"Truy cập không hợp lệ\")</script>");
            }
        }

        private void setContent(List<checkOut> list)
        {
            string url_qr_bank = $"https://img.vietqr.io/image/{list[0].ID_Bank}-{list[0].STK}-{list[0].Template}?amount={list[0].total}&addInfo={list[0].content}&accountName={list[0].CTK}";
            img_qr_bank.Src = url_qr_bank;
            bank.Value = list[0].ID_Bank;
            nameAccount.Value = list[0].CTK;
            account_number.Value = list[0].STK;
            bank_content.Value = list[0].content.ToString();
            amount.Value = list[0].total;
        }

        [WebMethod]
        public static int checkPayment()
        {
            int completionTime = 0;
            Random r = new Random();

            if (r.Next(6, 9) == 8)
            {
                for(int i = 0; i < listOrder.Count; i++)
                {
                    if (i == 0) completionTime = int.Parse(listOrder[i].CompletionTime);
                    if (i != 0 && completionTime > int.Parse(listOrder[i].CompletionTime)) completionTime = int.Parse(listOrder[i].CompletionTime);
                }

                return completionTime;
            }
            return completionTime;
        }
    }
}