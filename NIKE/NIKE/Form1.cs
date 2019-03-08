using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace NIKE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //创建DBHelper对象
        DBHelper db = new DBHelper();

    


        //登录按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //接收文本框的值
            string yhm = this.tb_yonghuming.Text;
            string pwd = this.tb_pwd.Text;
            //非空判断
            if (yhm == "" || pwd == "")
            {
                MessageBox.Show("用户名或密码不能为空！", "验证错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //根据登录名查询用户信息
            string sql = string.Format("select * from Salesman where Mobile='{0}'", yhm);
            DataSet ds = db.getDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("用户不存在！", "登录失败！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ds.Tables[0].Rows[0]["Pwd"].ToString() != pwd)
            {
                MessageBox.Show("密码错误！", "登录失败！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //登录成功，保存登录信息
                LoginInfo.UserID = ds.Tables[0].Rows[0]["SalessmanID"].ToString();
                LoginInfo.LoginID = ds.Tables[0].Rows[0]["Mobile"].ToString();
                LoginInfo.UserName = ds.Tables[0].Rows[0]["Salesman-Name"].ToString();
                LoginInfo.RoleName = ds.Tables[0].Rows[0]["Role"].ToString();
                //根据员工角色，打开不同界面
                if (ds.Tables[0].Rows[0]["Role"].ToString() == "收银员")
                {
                    new CashForm().Show();
                    //关闭窗体
                   
                }
                else if (ds.Tables[0].Rows[0]["Role"].ToString() == "导购员")
                {
                    //new Main2().Show();
                    new Main().Show();
                }
                else 
                {
                    new Main().Show();
                    //关闭窗体
            

                }
                this.Hide();


            }








        }
    }
}
