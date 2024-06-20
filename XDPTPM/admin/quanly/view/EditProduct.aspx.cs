using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XDPTPM.admin.quanly.assets.model;

namespace XDPTPM.admin.quanly.view
{
    public partial class EditProduct : System.Web.UI.Page
    {
        protected bool IsUpdate => !string.IsNullOrEmpty(Request.QueryString["ID"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["loginToken"].ToString() == "" || Session["loginToken"].ToString() == null)
                {
                    Response.Redirect("../../login/index.aspx");
                }
            }
            catch(Exception ex)
            {
                Response.Redirect("../../login/index.aspx");
            }
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        int productId = Convert.ToInt32(Request.QueryString["ID"]);
                        LoadCategories();
                        LoadUnit();

                        LoadProduct(productId);

                    }
                    else
                    {
                        LoadCategories();
                        LoadUnit();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void LoadProduct(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;

            string query = "SELECT * FROM tbl_Product WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", productId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtProductName.Text = reader["Name"].ToString();
                    ddlUnit.SelectedValue = reader["UnitID"].ToString();
                    txtProductPrice.Text = reader["Price"].ToString();
                    txtProductCompletionTime.Text = reader["CompletionTime"].ToString();
                    ddlStatus.SelectedValue = reader["Status"] != DBNull.Value && Convert.ToBoolean(reader["Status"]) ? "true" : "false";
                    ddlCategory.SelectedValue = reader["ClassifyID"].ToString();
                }
                conn.Close();
            }
        }

        private void LoadCategories()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;

            string query = "SELECT * FROM tbl_Classify";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                ddlCategory.DataSource = reader;
                ddlCategory.DataTextField = "Value";
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataBind();

                reader.Close();
            }
            ddlCategory.Items.Insert(0, new ListItem("-- Chọn loại sản phẩm --", ""));
        }
        private void LoadUnit()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;

            string query = "SELECT * FROM tbl_Unit";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                ddlUnit.DataSource = reader;
                ddlUnit.DataTextField = "Value";
                ddlUnit.DataValueField = "ID";
                ddlUnit.DataBind();

                reader.Close();
            }
            ddlUnit.Items.Insert(0, new ListItem("-- Chọn đơn vị tính --", ""));
        }
        private void AddProduct()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;
            string query = "INSERT INTO tbl_Product (Name, UnitID, Price, CompletionTime, ImagePath, Status, ClassifyID) VALUES (@Name, @UnitID, @Price, @CompletionTime, @ImagePath, @Status,@ClassifyID)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@UnitID", Convert.ToInt32(ddlUnit.SelectedValue));
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtProductPrice.Text));
                cmd.Parameters.AddWithValue("@CompletionTime", Convert.ToInt32(txtProductCompletionTime.Text));
                cmd.Parameters.AddWithValue("@ImagePath", SaveImageFile());
                cmd.Parameters.AddWithValue("@Status", Convert.ToBoolean(ddlStatus.SelectedValue));
                cmd.Parameters.AddWithValue("@ClassifyID", Convert.ToInt32(ddlCategory.SelectedValue));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        private void UpdateProduct(int productId)
        {
            string query = "";
            string imagePath = SaveImageFile();
            if (imagePath == null || imagePath == "") query = "UPDATE tbl_Product SET Name = @Name, UnitID = @UnitID, Price = @Price, CompletionTime = @CompletionTime, Status = @Status, ClassifyID = @ClassifyID WHERE ID = @ID";
            else
            {
                DeleteImage(productId);
                query = "UPDATE tbl_Product SET Name = @Name, UnitID = @UnitID, Price = @Price, CompletionTime = @CompletionTime, ImagePath = @ImagePath, Status = @Status, ClassifyID = @ClassifyID WHERE ID = @ID";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", productId);
                cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@UnitID", Convert.ToInt32(ddlUnit.SelectedValue));
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtProductPrice.Text));
                cmd.Parameters.AddWithValue("@CompletionTime", Convert.ToInt32(txtProductCompletionTime.Text));
                cmd.Parameters.AddWithValue("@ImagePath", SaveImageFile());
                cmd.Parameters.AddWithValue("@Status", Convert.ToBoolean(ddlStatus.SelectedValue));
                cmd.Parameters.AddWithValue("@ClassifyID", Convert.ToInt32(ddlCategory.SelectedValue));

                cmd.ExecuteNonQuery();

                conn.Close();
            }


        }
        private void DeleteImage(int productID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string querySelectImagePath = "SELECT ImagePath FROM tbl_Product WHERE ID = @ID";
                using (SqlCommand cmdSelectImagePath = new SqlCommand(querySelectImagePath, conn))
                {
                    cmdSelectImagePath.Parameters.AddWithValue("@ID", productID);
                    using (SqlDataReader reader = cmdSelectImagePath.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string imagePathOld = reader.GetString(0);
                            if (imagePathOld != null || imagePathOld != "")
                            {
                                string physicalPath = Server.MapPath(imagePathOld);
                                if (File.Exists(physicalPath))
                                {
                                    File.Delete(physicalPath);
                                }
                            }
                        }
                    }
                }
            }
        }
        private string SaveImageFile()
        {
            if (fileUploadProductImage.HasFile)
            {
                string fileName = Path.GetFileName(fileUploadProductImage.PostedFile.FileName);
                string filePath = Server.MapPath("../../../ProductImage/") + fileName;
                fileUploadProductImage.SaveAs(filePath);
                return "../../../ProductImage/" + fileName;
            }
            return string.Empty;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                int productId = Convert.ToInt32(Request.QueryString["ID"]);
                UpdateProduct(productId);
            }
            else
            {
                AddProduct();
            }

            Response.Redirect("Products.aspx");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("Products.aspx");
        }
    }
}
