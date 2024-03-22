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
    public partial class Viewproducts : System.Web.UI.Page
    {
        concls obj = new concls();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string str = "select * from Product where Categoryid="+Session["uid"]+"";
                DataSet ds = obj.Fn_Dataset(str);
                DataList1.DataSource = ds;
                DataList1.DataBind();
            }
        }

        protected void ImageButton2_Command(object sender, CommandEventArgs e)
        {
            int Productid1 = Convert.ToInt32(e.CommandArgument);
            Session["uid"] = Productid1;
            Response.Redirect("Productdetails.aspx");
        }
    }
}