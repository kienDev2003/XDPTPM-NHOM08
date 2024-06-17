using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM.client.cart
{
    public partial class index : System.Web.UI.Page
    {
        static DbConnection connection = new DbConnection();
        static List<order_dish> order_Dishes = new List<order_dish>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    
                    HttpCookie listCookie = Request.Cookies["OrderList"];
                    string stringListOrder = listCookie.Value;
                    order_Dishes = JsonConvert.DeserializeObject<List<order_dish>>(stringListOrder);
                    getOrderDish(order_Dishes);

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert(\"Truy cập không hợp lệ.\")</script>");
                }
            }
        }

        public void getOrderDish(List<order_dish> order_dish)
        {

            table_Content.Controls.Clear();
            string contentHtml = "";
            int totalAll = 0;

            foreach (var dish in order_dish)
            {
                string productID = dish.ProductID;
                int quantity = int.Parse(dish.Quantity);
                int priceProduct = 0;
                string imagePath = "";
                string name = "";

                connection.getConnection();

                string query = "SELECT Price,ImagePath,Name FROM tbl_Product WHERE ID = @productID";
                using (SqlCommand cmd = new SqlCommand(query, connection.conn))
                {
                    cmd.Parameters.AddWithValue("@productID", productID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            priceProduct = reader.GetInt32(0);
                            imagePath = reader.GetString(1);
                            name = reader.GetString(2);
                        }
                    }
                }
                string total = (quantity * priceProduct).ToString();
                totalAll += int.Parse(total);

                string temphtml = $"<tr><td class=\"p-4\"><div class=\"media align-items-center\"><img src=\"{imagePath}\" class=\"d-block ui-w-40 ui-bordered mr-4\" alt=\"\"><div class=\"media-body\"><a href=\"#\" class=\"d-block text-dark\">{name}</a></div></div></td><td class=\"text-right font-weight-semibold align-middle p-4\">{priceProduct}</td><td class=\"align-middle p-4\"><input type=\"number\" min=\"0\" id=\"txt_{productID}\" class=\"form-control text-center\" value=\"{quantity}\"></td><td class=\"text-right font-weight-semibold align-middle p-4\">{total}</td><td class=\"text-center align-middle px-0\"><input type=\"button\" id=\"btn_Remove_{productID}\" class=\"shop-tooltip close float-none text-danger\" value=\"Remove\"></td></tr>";
                contentHtml += temphtml;
                connection.closeConnection();
            }
            LiteralControl literalControl = new LiteralControl(contentHtml);
            table_Content.Controls.Add(literalControl);
            txtTotalPrice.InnerText = totalAll.ToString();

        }

        [WebMethod]
        public static string changeQuantityDish(string productID, string quantityNew)
        {
            order_Dishes = ReturnListOrderDish();

            productID = productID.Replace("txt_", "");
            if (int.Parse(quantityNew) < 0) quantityNew = 0.ToString();
            foreach (var quantity in order_Dishes)
            {
                if (quantity.ProductID == productID)
                {
                    quantity.Quantity = quantityNew;
                }
            }
            string contentHtml = "";
            int totalAll = 0;

            foreach (var dish in order_Dishes)
            {
                productID = dish.ProductID;
                int quantity = int.Parse(dish.Quantity);
                int priceProduct = 0;
                string imagePath = "";
                string name = "";

                connection.getConnection();

                string query = "SELECT Price,ImagePath,Name FROM tbl_Product WHERE ID = @productID";
                using (SqlCommand cmd = new SqlCommand(query, connection.conn))
                {
                    cmd.Parameters.AddWithValue("@productID", productID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            priceProduct = reader.GetInt32(0);
                            imagePath = reader.GetString(1);
                            name = reader.GetString(2);
                        }
                    }
                }
                string total = (quantity * priceProduct).ToString();
                totalAll += int.Parse(total);

                string temphtml = $"<tr><td class=\"p-4\"><div class=\"media align-items-center\"><img src=\"{imagePath}\" class=\"d-block ui-w-40 ui-bordered mr-4\" alt=\"\"><div class=\"media-body\"><a href=\"#\" class=\"d-block text-dark\">{name}</a></div></div></td><td class=\"text-right font-weight-semibold align-middle p-4\">{priceProduct}</td><td class=\"align-middle p-4\"><input type=\"number\" min=\"0\" id=\"txt_{productID}\" class=\"form-control text-center\" value=\"{quantity}\"></td><td class=\"text-right font-weight-semibold align-middle p-4\">{total}</td><td class=\"text-center align-middle px-0\"><input type=\"button\" id=\"btn_Remove_{productID}\" class=\"shop-tooltip close float-none text-danger\" value=\"Remove\"></td></tr>";
                contentHtml += temphtml;
                connection.closeConnection();
            }

            var result = new
            {
                ContentHtml = contentHtml,
                TotalAll = totalAll
            };
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(result);
        }

        [WebMethod]
        public static bool deleteDish(string productID)
        {
            order_Dishes = ReturnListOrderDish();

            productID = productID.Replace("btn_Remove_", "");
            for (int i = 0; order_Dishes.Count > 0; i++)
            {
                if (order_Dishes[i].ProductID == productID)
                {
                    order_Dishes.Remove(order_Dishes[i]);
                    HttpCookie jsonCookie = new HttpCookie("OrderList");
                    jsonCookie.Expires = DateTime.Now.AddMinutes(20);
                    string json = JsonConvert.SerializeObject(order_Dishes);
                    jsonCookie.Value = json;
                    HttpContext.Current.Response.Cookies.Add(jsonCookie);
                    return true;
                }
            }
            return false;
        }

        protected void btn_checkout_ServerClick(object sender, EventArgs e)
        {
            Mutex _checkoutMutex = new Mutex(false, Session.SessionID.ToString());
            try
            {
                // Chờ để được phép truy cập
                if (!_checkoutMutex.WaitOne(TimeSpan.FromSeconds(5))) // Chờ tối đa 5 giây
                {
                    return;
                }

                try
                {
                    order_Dishes = ReturnListOrderDish();

                    string total = txtTotalPrice.InnerText;
                    string TableID = order_Dishes[0].TableID.ToString();
                    string CTK = ConfigurationManager.AppSettings["CTK"].ToString();
                    string STK = ConfigurationManager.AppSettings["STK"].ToString();
                    string ID_Bank = ConfigurationManager.AppSettings["ID_Bank"].ToString();
                    string Template = ConfigurationManager.AppSettings["Template"].ToString();
                    string content = $"{DateTime.Now.ToString("yyA-MMB-ddC-HHD-mmE-ssG-ffffU").Replace("-", "")}";

                    List<checkOut> list = new List<checkOut>();
                    list.Add(new checkOut { total = total, CTK = CTK, STK = STK, ID_Bank = ID_Bank, Template = Template, content = content, Table_ID = TableID });

                    string json = JsonConvert.SerializeObject(list);
                    HttpCookie jsonCookie = new HttpCookie("checkOut");
                    jsonCookie.Expires = DateTime.Now.AddMinutes(20);
                    jsonCookie.Value = json;
                    Response.Cookies.Add(jsonCookie);

                    Response.Redirect("../checkout/index.aspx");
                }
                finally
                {
                    // Cho phép luồng khác truy cập
                    _checkoutMutex.ReleaseMutex();
                }

                return;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return; // Hoặc mã lỗi khác
            }
            

        }

        private static List<order_dish> ReturnListOrderDish()
        {
            HttpCookie listCookie = HttpContext.Current.Request.Cookies["OrderList"];
            string stringListOrder = listCookie.Value;
            order_Dishes = JsonConvert.DeserializeObject<List<order_dish>>(stringListOrder);
            return order_Dishes;
        }
    }
}