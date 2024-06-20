using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM.client.checkout
{
    public partial class index : System.Web.UI.Page
    {
        static DbConnection connection = new DbConnection();
        static Manage_cookies manage_Cookies = new Manage_cookies();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<order_dish> listOrder = new List<order_dish>();
            List<checkOut> list = new List<checkOut>();

            if (!IsPostBack)
            {
                try
                {
                    list = manage_Cookies.ReturnListCheckOut();
                    listOrder = manage_Cookies.ReturnOrderList();

                    setContent(list);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert(\"Vui lòng load lại trang !\")</script>");
                }
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
            List<order_dish> listOrder = new List<order_dish>();
            List<checkOut> list = new List<checkOut>();

            int completionTimeAll = 0;
            try
            {
                list = manage_Cookies.ReturnListCheckOut();
                listOrder = manage_Cookies.ReturnOrderList();

                Random r = new Random();
                if (r.Next(0, 9) == 6)
                {
                    string OrderID = DateTime.Now.ToString("HH-mm-ss").Replace("-", "");
                    string OrderDetailsID = OrderID + "9";
                    string AmountOfMoney = list[0].total.ToString();
                    string OrderTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string TableID = list[0].Table_ID;
                    bool status = true;

                    connection.getConnection();

                    for (int i = 0; i < listOrder.Count; i++)
                    {

                        string productID = listOrder[i].ProductID;
                        string quantity = listOrder[i].Quantity;
                        string completionTime = listOrder[i].CompletionTime;

                        string queryOrderDetails = $"INSERT INTO tbl_OrderDetails (ID,ProductID,Quantity,CompletionTime) VALUES ('{OrderDetailsID}','{productID}','{quantity}','{completionTime}');";
                        int cmdOrderDetails = connection.Command(queryOrderDetails);

                        if (i == 0) completionTimeAll = int.Parse(listOrder[i].CompletionTime);
                        if (i != 0 && completionTimeAll > int.Parse(listOrder[i].CompletionTime)) completionTimeAll = int.Parse(listOrder[i].CompletionTime);
                    }

                    string queryOrder = $"INSERT INTO tbl_Order (ID,TableID,OrderDetailsID,OrderTime,CompletionTime,Status,AmountOfMoney) VALUES ('{OrderID}','{TableID}','{OrderDetailsID}','{OrderTime}','{completionTimeAll}','{status}','{AmountOfMoney}');";
                    int cmdOrder = connection.Command(queryOrder);

                    connection.closeConnection();

                    manage_Cookies.RemoveAllCookie();

                }
                return completionTimeAll;
            }
            catch (Exception ex)
            {
                return completionTimeAll;
            }
        }
    }
}