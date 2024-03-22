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
    public partial class Productdetails : System.Web.UI.Page
    {
        concls obj = new concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "select Productname,Productimage,Productprice,Productdescription,Productstock from Product where Productid="+Session["uid"]+"";
            SqlDataReader dr = obj.Fn_Reader(str);
            while (dr.Read())
            {
                Label6.Text = dr["Productname"].ToString();
                Image1.ImageUrl = dr["Productimage"].ToString();
                Label7.Text = dr["Productprice"].ToString();
                Label8.Text = dr["Productdescription"].ToString();
               // TextBox1.Text = dr["Productstock"].ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(cartid) from Cart";
            string regid = obj.Fn_scalar(sel);
            int reg_id = 0;
            if (regid == "")
            {
                reg_id = 1;
            }
            else
            {
                int newregid = Convert.ToInt32(regid);
                reg_id = newregid + 1;

            }
            string str1 = "select Productprice from Product where Productid="+Session["uid"]+"";
            string str2 = obj.Fn_scalar(str1);
            int tot = (Convert.ToInt32(str2)) * (Convert.ToInt32(TextBox1.Text));
            string str = "insert into cart values("+reg_id+","+Session["userid"]+","+Session["uid"]+","+TextBox1.Text+","+tot+")";
            int i = obj.Fn_Nonquery(str);
            if (i != 0)
            {
                Label9.Text = "inserted";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}