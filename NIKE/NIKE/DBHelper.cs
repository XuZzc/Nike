using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace NIKE
{
    class DBHelper
    {
        //创建  SqlConnection 对象，连接数据库
        SqlConnection conn = new SqlConnection("server=.;database=NIKE;uid=sa;pwd=123456;");

        //查询方法
        public DataSet getDataSet(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            dap.Fill(ds, "info");
            return ds;
        }

        //增删改方法
        public int zsg(string sql)
        {
            //打开数据库
            conn.Open();
            //创建SqlCommand对象
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rows = cmd.ExecuteNonQuery();
            //关闭数据库
            conn.Close();
            return rows;
        }

        //登录查询
        public int denglu(string sql)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            int result = (int)cmd.ExecuteScalar();
            conn.Close();
            return result;

        }
        public  object ExecuteScalar(string sqlStr)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            object result = cmd.ExecuteScalar();
            conn.Close();
            return result;
        }

    }
}
