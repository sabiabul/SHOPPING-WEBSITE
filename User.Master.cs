﻿using System;
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
    public partial class User : System.Web.UI.MasterPage
    {
        SqlConnection con=new SqlConnection("Data Source=LAPTOP-22LD9A1M\\SQLEXPRESS;Initial Catalog=MyEShoppingDB;Integrated Security=True");
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Username"] != null)
            {
                //lblSuccess.Text = "Login Success, Welcome <b>" + Session["Username"].ToString() + "</b>";
                btnlogout.Visible = true;
                btnLogin.Visible = false;
                BindCartNumber22();
                Button1.Text = "Welcome: " + Session["Username"].ToString().ToUpper();

            }
            else
            {
                btnlogout.Visible = false;
                btnLogin.Visible = true;
                //Response.Redirect("~/Default.aspx");

            }
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;

            Response.Redirect("Default.aspx");

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SignIn.aspx");
        }
        public void BindCartNumber()
        {
            if (Request.Cookies["CartPID"] != null)
            {
                string CookiePID = Request.Cookies["CartPID"].Value.Split('=')[1];
                string[] ProductArray = CookiePID.Split(',');
                int ProductCount = ProductArray.Length;
                pCount.InnerText = ProductCount.ToString();
            }
            else
            {
                pCount.InnerText = 0.ToString();
            }
        }

        public void BindCartNumber22()
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
                            pCount.InnerText = CartQuantity;

                        }
                        else
                        {
                            pCount.InnerText = 0.ToString();
                        }
                    }
                }
            }
        }



    }
}