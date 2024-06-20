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
        static Manage_cookies manage_Cookies = new Manage_cookies();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<order_dish> order_s = new List<order_dish>();

            if (!IsPostBack)
            {
                try
                {
                    string TableID = Request.QueryString["tableID"].ToString();
                    Session["tableID"] = TableID;

                    try
                    {
                        string classifyID = Request.QueryString["classify"].ToString();
                        getListDish(int.Parse(classifyID));
                    }catch(Exception ex)
                    {
                        getListDish(0);
                    }
                    getListMenuClassify();
                    if (Request.Cookies["OrderList"] != null) order_s = manage_Cookies.ReturnOrderList();
                    manage_Cookies.AddCookieOrder("OrderList", order_s, 20);

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert(\"Không có mã bàn !.\")</script>");
                    return;
                }

            }
            cart_notice.InnerText = order_s.Count.ToString();
        }

        private void getListMenuClassify()
        {
            list_menu_classify.Controls.Clear();
            string lis = "";

            connection.getConnection();

            string query = "SELECT * FROM tbl_Classify";
            using(SqlDataReader dataReader = connection.DataReader(query))
            {
                while (dataReader.Read())
                {
                    string id = dataReader.GetInt32(0).ToString();
                    string value = dataReader.GetString(1);

                    string temp = $"<li><a class=\"a_classify\" id=\"{id}\" href=\"\">{value}</a></li>";
                    lis += temp;
                }
                LiteralControl literalControl = new LiteralControl(lis);
                list_menu_classify.Controls.Add(literalControl);
            }

            connection.closeConnection();
        }

        public void getListDish(int classifyID)
        {
            list_dish.Controls.Clear();

            string itemHtml = "";

            connection.getConnection();
            string query = "";
            if (classifyID == 0) query = $"SELECT * FROM tbl_Product WHERE Status = 1;";
            else query = $"SELECT * FROM tbl_Product WHERE Status = 1 AND ClassifyID = {classifyID};";
            using (SqlDataReader dataReader = connection.DataReader(query))
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

            connection.closeConnection();
        }

        [WebMethod]
        public static string addDishToOrder(string ProductID)
        {
            List<order_dish> order_s = new List<order_dish>();

            order_s = manage_Cookies.ReturnOrderList();

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

                string sql = $"SELECT CompletionTime FROM tbl_Product WHERE ID = '{ProductID}'";
                using (SqlDataReader reader = connection.DataReader(sql))
                {
                    if (reader.Read())
                    {
                        string temp = reader.GetInt32(0).ToString();
                        completionTime = int.Parse(temp);
                    }
                }

                connection.closeConnection();

                order_s.Add(new order_dish { TableID = HttpContext.Current.Session["tableID"].ToString(), ProductID = ProductID, Quantity = 1.ToString(), CompletionTime = completionTime.ToString() });

                manage_Cookies.AddCookieOrder("OrderList",order_s, 20);

                return order_s.Count.ToString();
            }
            catch (Exception ex)
            {
                return "error";
            }
        }
    }
}