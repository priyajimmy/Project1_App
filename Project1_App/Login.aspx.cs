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
    public partial class Login : System.Web.UI.Page
    {
        concls obj = new concls();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "select count(Reg_id) from Login_tab where Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
            String cid = obj.Fn_scalar(str);
            int cid1 = Convert.ToInt32(cid);
            if (cid1 == 1)
            {
                string str1 = "select Reg_id from Login_tab where Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
                string regid = obj.Fn_scalar(str1);
                Session["userid"] = regid;
                string str2 = "select Log_type from Login_tab where Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
                string logtype = obj.Fn_scalar(str2);
                if (logtype == "admin")
                {
                    Response.Redirect("Admin_Home.aspx");
                }
                else if (logtype == "user")
                {
                    string str3 = "select Usstatus from Userreg where Usid=" + Session["userid"] + "";//need to correct this code
                    string Usstatus1 = obj.Fn_scalar(str3);
                    if (Usstatus1 == "1")
                    {
                        Response.Redirect("User_Home.aspx");
                    }
                    else
                    {
                        Label3.Text = "Not an active user";
                    }

                }
            }
        }
    }
}
















































































































































































































































































