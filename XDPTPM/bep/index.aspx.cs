using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.Services;

namespace XDPTPM.bep
{
    public partial class index : System.Web.UI.Page
    {
        static DbConnection connection = new DbConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            string itemHtml = getListOrder();
            list_order.Controls.Clear();
            LiteralControl literalControl = new LiteralControl(itemHtml);
            list_order.Controls.Add(literalControl);
        }

        [WebMethod]
        public static string getListOrder()
        {
            string itemHtml = "";

            connection.getConnection();

            string query = "SELECT * FROM tbl_Order WHERE Status = 1 ORDER BY OrderTime ASC;";
            using (SqlCommand cmd = new SqlCommand(query, connection.conn))
            {
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int ID = dataReader.GetInt32(0);
                        int tableID = dataReader.GetInt32(1);
                        int orderDetailsID = dataReader.GetInt32(2);

                        string itemTemp = $"<li class=\"item\"><a href=\"orderDetails.aspx?orderDetailsID={orderDetailsID}&tableID={tableID}&orderID={ID}\"><img src=\"./assets/img/images (1).png\"/><p>Bàn số {tableID}</p></a></li>";
                        itemHtml += itemTemp;
                    }
                }
            }

            connection.closeConnection();

            return itemHtml;
        }
    }
}