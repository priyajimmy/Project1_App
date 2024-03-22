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
    public partial class Add_Category : System.Web.UI.Page
    {
        concls obj = new concls();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = "~/Categoryimages/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(s));
            string categoryins = "insert into Category values('"+TextBox1.Text+ "','"+s+"','"+ TextBox2.Text + "',1)";
            int i = obj.Fn_Nonquery(categoryins);
            if (i != 0)
            {
                Label4.Text = "Inserted";
            }
        }
    }
}