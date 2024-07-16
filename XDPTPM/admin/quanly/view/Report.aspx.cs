using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XDPTPM.admin.quanly.view
{
    public partial class Report : System.Web.UI.Page
    {
        static DbConnection connection = new DbConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime datetime = DateTime.Now;
                lastDate.Value = datetime.AddDays(-7).ToString("yyyy-MM-dd");
                date.Value = datetime.ToString("yyyy-MM-dd");
                getListReport(lastDate.Value, date.Value);
            }

        }

        private void getListReport(string lastDate, string date)
        {
            connection.getConnection();

            string query = $"SELECT TableID,OrderTime,AmountOfMoney FROM tbl_Order WHERE '{lastDate} 00:00:00' <= OrderTime AND OrderTime <= '{date} 23:59:59';";

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection.conn))
            {
                DataTable table = new DataTable();
                dataAdapter.Fill(table);

                gvReport.DataSource = table;
                gvReport.DataBind();

                int total = 0;

                foreach (DataRow row in table.Rows)
                {
                    int amount = Convert.ToInt32(row["AmountOfMoney"]);
                    total += amount;
                }

                txtAllMoney.InnerText = $"Doanh Thu: {total.ToString()}"; 
            }

            connection.closeConnection();
        }

        protected void btnSreach_ServerClick(object sender, EventArgs e)
        {
            string _lastDate = lastDate.Value;
            string _date = date.Value;

            getListReport(_lastDate, _date);

            lastDate.Value = _lastDate;
            date.Value = _date;
        }
    }
}