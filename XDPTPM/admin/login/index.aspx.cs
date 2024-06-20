using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM.admin.login
{
    public partial class index : System.Web.UI.Page
    {
        DbConnection connection = new DbConnection();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_ServerClick(object sender, EventArgs e)
        {
            string username = txtUsername.Value;
            string password = txtPassword.Value;

            if(username == "" || password == "")
            {
                Response.Write("<script>alert(\"Vui lòng nhập đầy đủ thông tin\")</script>");
                return;
            }

            connection.getConnection();

            string query = "SELECT * FROM tbl_Accounts WHERE Username = @username AND Password = @password";
            using(SqlCommand cmd = new SqlCommand(query,connection.conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        Session["loginToken"] = Guid.NewGuid().ToString();
                        Response.Redirect("../quanly/view/HomeAdmin.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert(\"Sai Username hoặc Password. Vui lòng kiểm tra lại\")</script>");
                    }
                }
            }

            connection.closeConnection();
        }
    }
}