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
    public partial class Viewcart : System.Web.UI.Page
    {
        concls obj = new concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_Grid();
            }

        }
        public void Bind_Grid()
        {
            string sel = "select * from Cart";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Bind_Grid();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Bind_Grid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox txtquantity = (TextBox)GridView1.Rows[i].Cells[4].Controls[0];

            string str1 = "select Productprice from Product where Productid=" + Session["uid"] + "";
            string str2 = obj.Fn_scalar(str1);
            int tot = (Convert.ToInt32(txtquantity.Text)) * (Convert.ToInt32(str2));
            string strup1 = "update Cart set Quantity=" + txtquantity.Text + ",Total=" + tot + " where Cartid=" + getid + "";
            int k = obj.Fn_Nonquery(strup1);
            if (k != 0)
            {
                GridView1.EditIndex = -1;
                Bind_Grid();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = "select max(Cartid) from Cart";
            string s1 = obj.Fn_scalar(s);
            int n = Convert.ToInt32(s1);
            int quant = 0, Tot = 0, cart_id = 0, pro_id = 0, regid = 0;
            for (int i = 1; i <= n; i++)
            {
                string sel = "select * from Cart where Cartid=" + i + "";
                SqlDataReader dr = obj.Fn_Reader(sel);
                while (dr.Read())
                {
                    regid = Convert.ToInt32(dr["Usid"].ToString());
                    pro_id = Convert.ToInt32(dr["Productid"].ToString());
                    cart_id = Convert.ToInt32(dr["Cartid"].ToString());
                    quant = Convert.ToInt32(dr["Quantity"].ToString());
                    Tot = Convert.ToInt32(dr["Total"].ToString());
                   // string ui = dr["cartid"].ToString();
                }
                string r = Session["userid"].ToString();
                string u = regid.ToString();
                if (u == r)
                {
                    //string st = "select max(Orderid) from Orderr";
                    //string ordid = obj.Fn_scalar(st);
                    //int orderid = 0;
                    //if (ordid == "")
                    //{
                    //    orderid = 1;
                    //}
                    //else
                    //{
                    //    int neworder_id = Convert.ToInt32(ordid);
                    //    orderid = neworder_id + 1;
                    //}
                    var orderdate = DateTime.Now.ToShortDateString();
                    string neworderdate = Convert.ToDateTime(orderdate).ToString("yyyy-MM-dd");
                    string strinsor = "insert into Orderr values(" + regid + "," + Session["uid"] + "," + quant + "," + Tot + ",1,'" + neworderdate + "')";
                    int k = obj.Fn_Nonquery(strinsor);
                    if (k != 0)
                    {
                        string del = "delete from Cart where Cartid=" + cart_id + "";
                        int d = obj.Fn_Nonquery(del);
                    }
                }
            }
            string str1 = "select sum(Total) from Orderr where Usid=" + Session["userid"] + " and Orderstatus=1";
            string Gtotal = obj.Fn_scalar(str1);
            Session["gtot"] = Gtotal;
            string s3 = "select max(Billid) from Bill";
            string bid = obj.Fn_scalar(s3);
            int bill_id = 0;
            if (bid == "")
            {
                bill_id = 1;
            }
            else
            {
                int newbillid = Convert.ToInt32(bid);
                bill_id = newbillid + 1;
            }
            var billdate = DateTime.Now.ToShortDateString();
            string newdate = Convert.ToDateTime(billdate).ToString("yyyy-MM-dd");
            string insbill = "insert into Bill values(" + bill_id + "," + Session["userid"] + ",'" + newdate + "'," + Session["gtot"] + ",1)";
            int b = obj.Fn_Nonquery(insbill);
           Response.Redirect("Viewbill.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Home.aspx");
        }
    }
    }
    
  //  }
