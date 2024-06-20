﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;
using XDPTPM.admin.quanly.assets.model;
using System.IO;

namespace XDPTPM.admin.quanly.view
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        static DbConnection connection = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["loginToken"].ToString() == "" || Session["loginToken"].ToString() == null)
                {
                    Response.Redirect("../../login/index.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../../login/index.aspx");
            }
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Thực hiện truy vấn
                using (SqlCommand cmd = new SqlCommand("_selectAdminProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    // Hiển thị danh sách sản phẩm
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }



        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteProduct")
            {
                int productId = Convert.ToInt32(e.CommandArgument);

                DeleteProduct(productId);
                BindGridView();

            }

            if (e.CommandName == "EditProduct")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("./EditProduct.aspx?ID=" + productId);
            }
        }

        private void DeleteProduct(int productId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM tbl_Product WHERE ID = @ID";
                    string querySelectImagePath = "SELECT ImagePath FROM tbl_Product WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlCommand cmdSelectImagePath = new SqlCommand(querySelectImagePath, conn))
                        {
                            cmdSelectImagePath.Parameters.AddWithValue("@ID", productId);
                            using (SqlDataReader reader = cmdSelectImagePath.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string imagePath = reader.GetString(0);
                                    if (imagePath != null || imagePath != "")
                                    {
                                        string physicalPath = Server.MapPath(imagePath);
                                        if (File.Exists(physicalPath))
                                        {
                                            File.Delete(physicalPath);
                                        }
                                    }
                                }
                            }
                        }

                        cmd.Parameters.AddWithValue("@ID", productId);
                        cmd.ExecuteNonQuery();

                        BindGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa sản phẩm không thành công : " + ex.Message);
            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}