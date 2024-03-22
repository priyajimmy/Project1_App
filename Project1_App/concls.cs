using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace Project1_App
{
    public class concls
    {
        SqlCommand cmd;
        SqlConnection Con;
        public concls()
        {
            Con = new SqlConnection(@"Server=LAPTOP-OLUFQ7H5\SQLEXPRESS;database=DbProject1;Integrated security=true");


        }
        public int Fn_Nonquery(string sqlquery)//insert,delete,update
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
            cmd = new SqlCommand(sqlquery, Con);
            Con.Open();
            int i = cmd.ExecuteNonQuery();
            Con.Close();
            return i;
        }
        public string Fn_scalar(string sqlquery)//AGGREGATE
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
            cmd = new SqlCommand(sqlquery, Con);
            Con.Open();
            string s = cmd.ExecuteScalar().ToString();
            Con.Close();
            return s;
        }
        public SqlDataReader Fn_Reader(string sqlquery)//SELECT
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
            cmd = new SqlCommand(sqlquery, Con);
            Con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public DataSet Fn_Dataset(string sqlquery)//SELECT
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, Con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataTable Fn_Datatable(string sqlquery)//SELECT
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}