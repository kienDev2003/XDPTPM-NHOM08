using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM.thucdon
{
    public partial class index : System.Web.UI.Page
    {
        static DbConnection connection = new DbConnection();
        static List<order_dish> order_s = new List<order_dish>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!IsPostBack)
            {
                try
                {
                    string TableID = Request.QueryString["tableID"].ToString();
                    Session["tableID"] = TableID;

                    getListDish();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert(\"Truy cập không hợp lệ.\")</script>");
                    return;
                }
                if (Request.Cookies["OrderList"] == null)
                {
                    HttpCookie jsonCookie = new HttpCookie("OrderList");
                    jsonCookie.Expires = DateTime.Now.AddMinutes(20);
                    string json = JsonConvert.SerializeObject(order_s);
                    jsonCookie.Value = json;
                    Response.Cookies.Add(jsonCookie);

                }
                else
                {
                    order_s = ReturnOrderList();
                }
            }
            cart_notice.InnerText = order_s.Count.ToString();
        }

        public void getListDish()
        {
            list_dish.Controls.Clear();

            string itemHtml = "";

            connection.getConnection();

            string query = "SELECT * FROM tbl_Product WHERE Status = 1;";
            using (SqlCommand cmd = new SqlCommand(query, connection.conn))
            {
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        string productID = dataReader.GetInt32(0).ToString();
                        string productName = dataReader.GetString(1);
                        string productPrice = dataReader.GetInt32(3).ToString();
                        string productImage = dataReader.GetString(5);

                        string itemTemp = $"<li class=\"item-dish\"><img src=\"{productImage}\" alt=\"\" class=\"img-dish\" /><p class=\"name-dish\">{productName}</p><p class=\"price-dish\">{productPrice}</p><input id=\"{productID}\" type=\"button\" value=\"Chọn món\" class=\"order-dish\" /></li>";
                        itemHtml += itemTemp;
                    }
                    LiteralControl literalControl = new LiteralControl(itemHtml);
                    list_dish.Controls.Add(literalControl);
                }
            }

            connection.closeConnection();

        }

        [WebMethod]
        public static string addDishToOrder(string ProductID)
        {
            order_s = ReturnOrderList();

            int completionTime = 0;
            try
            {
                foreach (var _order_s in order_s)
                {
                    if (_order_s.ProductID == ProductID)
                    {
                        _order_s.Quantity = (int.Parse(_order_s.Quantity) + 1).ToString();
                        return order_s.Count.ToString();
                    }
                }
                connection.getConnection();

                string sql = $"SELECT CompletionTime FROM tbl_Product WHERE ID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(sql, connection.conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", ProductID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string temp = reader.GetInt32(0).ToString();
                            completionTime = int.Parse(temp);
                        }
                    }
                }

                connection.closeConnection();

                order_s.Add(new order_dish { TableID = HttpContext.Current.Session["tableID"].ToString(), ProductID = ProductID, Quantity = 1.ToString(), CompletionTime = completionTime.ToString() });

                HttpCookie jsonCookie = new HttpCookie("OrderList");
                jsonCookie.Expires = DateTime.Now.AddMinutes(20);
                string json = JsonConvert.SerializeObject(order_s);
                jsonCookie.Value = json;
                HttpContext.Current.Response.Cookies.Add(jsonCookie);

                return order_s.Count.ToString();
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        private static List<order_dish> ReturnOrderList()
        {
            string json = HttpContext.Current.Request.Cookies["OrderList"].Value;
            order_s = JsonConvert.DeserializeObject<List<order_dish>>(json);
            return order_s;
        }
    }
}