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
    public partial class Add_Product : System.Web.UI.Page
    {
        concls ob = new concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str1 = "select Categoryid,Categoryname from Category";
                DataSet ds = ob.Fn_Dataset(str1);
                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "Categoryname";
                DropDownList1.DataValueField = "Categoryid";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "-select-");
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "";
            p = "~/Productphoto/" + FileUpload1.FileName ;
            FileUpload1.SaveAs(MapPath(p));
            string strins = "insert into Product values("+DropDownList1.SelectedItem.Value+",'"+TextBox1.Text+"','"+p+"',"+TextBox2.Text+",'"+TextBox3.Text+"',1,"+TextBox4.Text+")";
            int i = ob.Fn_Nonquery(strins);
            if (i != 0)
            {
                Label7.Text = "Inserted";
            }
        }
    }
}