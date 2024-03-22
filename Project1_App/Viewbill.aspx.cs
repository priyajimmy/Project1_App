using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace Project1_App
{
    public partial class Viewbill : System.Web.UI.Page
    {
        concls obj = new concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = "select Userreg.Usname,Userreg.Usaddress,Bill.Billdate,Bill.Grandtotal from Userreg join Bill on Bill.Usid=Userreg.Usid";

                SqlDataReader dr = obj.Fn_Reader(str);
                while (dr.Read())
                {
                    Label6.Text = dr["Usname"].ToString();
                    Label7.Text = dr["Usaddress"].ToString();
                    Label8.Text = dr["Billdate"].ToString();
                    Label10.Text = dr["Grandtotal"].ToString();
                }
                string str1 = "select count(Quantity) from Orderr where Usid='" + Session["userid"] + "' and Orderstatus=1";
                string quantity = obj.Fn_scalar(str1);
                Label9.Text = quantity;
            }

            }

            protected void Button1_Click(object sender, EventArgs e)
            {
                  Response.Redirect("Payment.aspx");
            }
        }
    }

