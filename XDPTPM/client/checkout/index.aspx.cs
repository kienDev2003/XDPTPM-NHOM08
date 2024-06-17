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
        static List<order_dish> listOrder = new List<order_dish>();
        static List<checkOut> list = new List<checkOut>();
        static DbConnection connection = new DbConnection();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    list = ReturnListCheckOut();
                    listOrder = ReturnListOrderDish();

                    setContent(list);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert(\"Truy cập không hợp lệ\")</script>");
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
            int completionTimeAll = 0;
            Mutex _checkoutMutex = new Mutex(false, HttpContext.Current.Session.SessionID.ToString());

            try
            {
                // Chờ để được phép truy cập
                if (!_checkoutMutex.WaitOne(TimeSpan.FromSeconds(5))) // Chờ tối đa 5 giây
                {
                    // Nếu không thể lấy được Mutex trong vòng 5 giây, 
                    // có thể trả về mã lỗi hoặc thông báo cho người dùng
                    return completionTimeAll; // Hoặc mã lỗi khác
                }

                try
                {
                    list = ReturnListCheckOut();
                    listOrder = ReturnListOrderDish();

                    Random r = new Random();
                    while (true)
                    {
                        if (r.Next(0, 9) == 6) break;
                    }
                    if (6 == 6)
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

                            string queryOrderDetails = $"INSERT INTO tbl_OrderDetails (ID,ProductID,Quantity,CompletionTime) VALUES (@ID,@ProductID,@Quantity,@CompletionTime);";
                            using (SqlCommand cmd = new SqlCommand(queryOrderDetails, connection.conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", OrderDetailsID);
                                cmd.Parameters.AddWithValue("@ProductID", productID);
                                cmd.Parameters.AddWithValue("@Quantity", quantity);
                                cmd.Parameters.AddWithValue("@CompletionTime", completionTime);

                                cmd.ExecuteNonQuery();
                            }

                            if (i == 0) completionTimeAll = int.Parse(listOrder[i].CompletionTime);
                            if (i != 0 && completionTimeAll > int.Parse(listOrder[i].CompletionTime)) completionTimeAll = int.Parse(listOrder[i].CompletionTime);
                        }

                        string queryOrder = "INSERT INTO tbl_Order (ID,TableID,OrderDetailsID,OrderTime,CompletionTime,Status,AmountOfMoney) VALUES (@ID,@TableID,@OrderDetailsID,@OrderTime,@CompletionTime,@Status,@AmountOfMoney)";
                        using (SqlCommand cmd = new SqlCommand(queryOrder, connection.conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", OrderID);
                            cmd.Parameters.AddWithValue("@TableID", TableID);
                            cmd.Parameters.AddWithValue("@OrderDetailsID", OrderDetailsID);
                            cmd.Parameters.AddWithValue("@OrderTime", OrderTime);
                            cmd.Parameters.AddWithValue("@CompletionTime", completionTimeAll);
                            cmd.Parameters.AddWithValue("@Status", status);
                            cmd.Parameters.AddWithValue("@AmountOfMoney", AmountOfMoney);

                            cmd.ExecuteNonQuery();
                        }

                        connection.closeConnection();

                        var cookieNames = new List<string>();
                        foreach (string cookieName in HttpContext.Current.Request.Cookies)
                        {
                            cookieNames.Add(cookieName);
                        }

                        foreach (string cookieName in cookieNames)
                        {
                            HttpCookie cookie = new HttpCookie(cookieName);
                            cookie.Expires = DateTime.Now.AddDays(-1);

                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                    }
                    return completionTimeAll;
                }
                finally
                {
                    // Cho phép luồng khác truy cập
                    _checkoutMutex.ReleaseMutex();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return completionTimeAll; // Hoặc mã lỗi khác
            }
        }

        private static List<order_dish> ReturnListOrderDish()
        {
            HttpCookie listCookie = HttpContext.Current.Request.Cookies["OrderList"];
            string stringListOrder = listCookie.Value;
            listOrder = JsonConvert.DeserializeObject<List<order_dish>>(stringListOrder);
            return listOrder;
        }

        private static List<checkOut> ReturnListCheckOut()
        {
            HttpCookie listCookie = HttpContext.Current.Request.Cookies["checkOut"];
            string stringListCheckout = listCookie.Value;
            list = JsonConvert.DeserializeObject<List<checkOut>>(stringListCheckout);
            return list;
        }
    }
}