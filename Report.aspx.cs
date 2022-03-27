using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SHOPPING_WEBSITE
{
    public partial class Report : System.Web.UI.Page
    {
        SqlConnection con=new SqlConnection("Data Source=LAPTOP-22LD9A1M\\SQLEXPRESS;Initial Catalog=MyEShoppingDB;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    bindGrid1();
                    bindGrid2();
                }
                else
                {
                    Response.Redirect("Signin.aspx");
                }
            }
        }
        private void bindGrid1()
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-22LD9A1M\\SQLEXPRESS;Initial Catalog=MyEShoppingDB;Integrated Security=True");
            string qr = "select t1.OrderID,t3.Name,t2.PName,t1.Quantity as QtySell,t4.Quantity as StockOpening,t4.Quantity-t1.Quantity as Available  from tblOrderProducts as t1 inner join tblProducts as t2 on t2.PID=t1.PID inner join tblUsers as t3 on t3.Uid=t1.UserID inner join tblProductSizeQuantity as t4 on t4.PID=t1.PID";
            SqlCommand cmd = new SqlCommand(qr, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void bindGrid2()
        {

            SqlConnection con = new SqlConnection("Data Source=LAPTOP-22LD9A1M\\SQLEXPRESS;Initial Catalog=MyEShoppingDB;Integrated Security=True");
            string qr = "select  distinct t2.PName,t1.Quantity from tblProductSizeQuantity as t1 inner join tblProducts as t2 on t2.PID=t1.PID";
            SqlCommand cmd = new SqlCommand(qr, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
    }
}