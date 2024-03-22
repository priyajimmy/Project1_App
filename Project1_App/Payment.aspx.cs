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
    public partial class Payment : System.Web.UI.Page
    {
        concls obj = new concls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s = "select max(Billid) from Bill where Usid="+Session["userid"]+"";
                string bill = obj.Fn_scalar(s);
                int b = Convert.ToInt32(bill);
                string str = "select Grandtotal from Bill where Billid="+b+"";
                string amount = obj.Fn_scalar(str);
                Session["amnt"] = amount;
                Label3.Text = amount;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            payservice.ServiceClient obj1 = new payservice.ServiceClient();
            string s = obj1.balancecheck(Convert.ToInt32(TextBox1.Text));
            int m = Convert.ToInt32(s);
            int t = Convert.ToInt32(Session["amnt"]);
            if (m > t)
            {
                string newbal = (m - t).ToString();
                payservice.ServiceClient obj2 = new payservice.ServiceClient();
                int b = obj2.balanceupdate(TextBox1.Text, newbal);
                if (b != 0)
                {
                    string s1 = "update Orderr set Orderstatus=1 where Usid="+Session["userid"]+"";
                    int c = obj.Fn_Nonquery(s1);
                    string s2 = "update Bill set Billstatus=1 where Usid=" + Session["userid"] + "";
                    int d = obj.Fn_Nonquery(s2);
                }
                Label4.Text = "successfully paid";
                string str = "select max(Orderid) from Orderr";
                string maxcartid = obj.Fn_scalar(str);
                int mcatid = Convert.ToInt32(maxcartid);
                int prdt_id = 0, reg_id = 0, qnty = 0, nw_stk = 0,status=0;
               for(int i = 1; i <= mcatid; i++)
                {
                    string sel1 = "select * from Orderr where Orderid="+i+"";
                    SqlDataReader dr = obj.Fn_Reader(sel1);
                    while (dr.Read())
                    {
                        prdt_id = Convert.ToInt32(dr["Productid"]);
                        reg_id = Convert.ToInt32(dr["Usid"]);
                        qnty = Convert.ToInt32(dr["Quantity"]);
                        status = Convert.ToInt32(dr["Orderstatus"]);

                    }
                    string r = Session["userid"].ToString();
                    string u = reg_id.ToString();
                    if (u == r)
                    {
                        if (status == 1)
                        {
                            string s2 = "select Productstock from Product where Productid="+prdt_id+"";
                            string st = obj.Fn_scalar(s2);
                            int k = Convert.ToInt32(st);
                            if (k > qnty)
                            {
                                nw_stk = k - qnty;

                            }
                            else
                            {
                                nw_stk = 0;
                            }
                            string s4 = "update Product set Productstock="+nw_stk+" where Productid="+prdt_id+"";
                            int j = obj.Fn_Nonquery(s4);
                            string s5 = "select Productstock from Product";
                            string t1 = obj.Fn_scalar(s5);
                            int sta = Convert.ToInt32(t1);
                            if (sta == 0)
                            {
                                string s6 = "update Product set Productstatus=0 where Productid="+prdt_id+"";
                                int x = obj.Fn_Nonquery(s6);
                            }
                        }
                    }
                        
                }
            }
            else
            {
                Label4.Text = "Insufficient Balance";
            }
        }
    }
}