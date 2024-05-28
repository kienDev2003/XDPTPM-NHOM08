using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace XDPTPM.bep
{
    public partial class orderDetails : System.Web.UI.Page
    {
        DbConnection connection = new DbConnection();
        int orderDetailsID;
        int tableID;
        int orderID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                orderDetailsID = int.Parse(Request.QueryString["orderDetailsID"].ToString());
                tableID = int.Parse(Request.QueryString["tableID"].ToString());
                orderID = int.Parse(Request.QueryString["orderID"].ToString());

                nameTable.InnerText = $"Chi tiết đơn bàn {tableID}";
                getOrderDetails(orderDetailsID);
            }
            catch (Exception ex)
            {

            }

        }

        private void getOrderDetails(int OrderDetailsID)
        {
            content_table.Controls.Clear();
            string contentHtml = "";

            connection.getConnection();

            using (SqlCommand cmd = new SqlCommand("_SelectOrderDetails", connection.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderDetailsID", orderDetailsID);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        string nameProduct = dataReader.GetString(0);
                        int quantity = dataReader.GetInt32(1);
                        string unit = dataReader.GetString(2);
                        int completionTime = dataReader.GetInt32(3);

                        string trTemp = $"<tr><th>{nameProduct}</th><th>{quantity}</th><th>{unit}</th><th>{completionTime} phút</th></tr>";
                        contentHtml += trTemp;
                    }

                    LiteralControl literalControl = new LiteralControl(contentHtml);
                    content_table.Controls.Add(literalControl);
                }
            }

            connection.closeConnection();
        }

        protected void btnSuccess_ServerClick(object sender, EventArgs e)
        {
            connection.getConnection();

            string query = $"UPDATE tbl_Order SET Status = 0 WHERE ID = @orderID";
            using(SqlCommand cmd = new SqlCommand(query, connection.conn))
            {
                cmd.Parameters.AddWithValue("@orderID",orderID);

                int check = cmd.ExecuteNonQuery();
                if(check > 0)
                {
                    Response.Redirect("./index.aspx");
                }
                else
                {
                    Response.Write("<script>alert(\"Lỗi chưa xác định, Vui lòng thử lại\")</script>");
                }
            }

            connection.closeConnection();
        }
    }
}