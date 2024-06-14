using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        static List<order_dish> order_s;
        static string TableID = "";
        static string OrderDetailsID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                order_s= new List<order_dish>();

                TableID = Request.QueryString["tableID"].ToString();
                OrderDetailsID = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                OrderDetailsID = OrderDetailsID.Replace("-", "");
                getListDish();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(\"Truy cập không hợp lệ.\")</script>");
                return;
            }
            if (Session["OrderList"] == null)
            {
                Session["OrderList"] = order_s;
            }
            else
            {
                order_s = (List<order_dish>)Session["OrderList"];
            }
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
                order_s.Add(new order_dish { OrderDetailsID = OrderDetailsID, TableID = TableID, ProductID = ProductID, Quantity = 1.ToString() });
                return order_s.Count.ToString(); ;
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
    }
}