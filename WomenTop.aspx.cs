using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace SHOPPING_WEBSITE
{
    public partial class WomenTop : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-22LD9A1M\\SQLEXPRESS;Initial Catalog=MyEShoppingDB;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Username"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["BuyNow"] == "YES")
                    {

                    }
                    BindProductRepeater();
                    BindCartNumber();

                }
            }
            else
            {
                if (Request.QueryString["BuyNow"] == "YES")
                {
                    Response.Redirect("SignIn.aspx");
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }


        private void BindProductRepeater()
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-22LD9A1M\\SQLEXPRESS;Initial Catalog=MyEShoppingDB;Integrated Security=True");
            {
                using (SqlCommand cmd = new SqlCommand("procBindAllProductsWomanTop", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptrProducts.DataSource = dt;
                        rptrProducts.DataBind();
                        if (dt.Rows.Count <= 0)
                        {
                            // Label1.Text = "Sorry! Currently no products in this category.";
                        }
                        else
                        {
                            //Label1.Text = "Showing All Products";
                        }
                    }
                }
            }

        }

        protected override void InitializeCulture()
        {
            CultureInfo ci = new CultureInfo("en-IN");
            ci.NumberFormat.CurrencySymbol = "₹";
            Thread.CurrentThread.CurrentCulture = ci;

            base.InitializeCulture();
        }
        public void BindCartNumber()
        {
            if (Session["USERID"] != null)
            {
                string UserIDD = Session["USERID"].ToString();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection("Data Source=LAPTOP-22LD9A1M\\SQLEXPRESS;Initial Catalog=MyEShoppingDB;Integrated Security=True");
                {
                    SqlCommand cmd = new SqlCommand("SP_BindCartNumberz", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@UserID", UserIDD);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string CartQuantity = dt.Compute("Sum(Qty)", "").ToString();
                            //CartBadge.InnerText = CartQuantity;
                        }
                        else
                        {
                            // _ = CartBadge.InnerText == 0.ToString();
                            //CartBadge.InnerText = "0";
                        }
                    }
                }
            }
        }

    }

}