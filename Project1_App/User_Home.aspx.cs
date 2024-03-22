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
    public partial class User_Home : System.Web.UI.Page
    {
        concls obj = new concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = "select * from Category";
                DataSet ds = obj.Fn_Dataset(str);
                DataList1.DataSource = ds;
                DataList1.DataBind();
            }
           
        }

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {
            int Categoryid1 = Convert.ToInt32(e.CommandArgument);
            Session["uid"] = Categoryid1;
            Response.Redirect("viewproducts.aspx");
        }
    }
}