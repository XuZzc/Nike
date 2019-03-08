using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NIKE
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }
        //创建DBHelper对象
        DBHelper db = new DBHelper();
        //加载员工
        private void Employee_Load(object sender, EventArgs e)
        {
            chaxun();
        }

        public void chaxun()
        {
            //清除控件中的所有数据
            this.listView1.Items.Clear();
            string sql = string.Format("select * from salesman");
            DataSet ds = db.getDataSet(sql);
            DataTable dt = ds.Tables[0];
            //遍历数据，绑定到控件中
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["SalessmanID"].ToString());
                item.SubItems.Add(row["Salesman-Name"].ToString());
                item.SubItems.Add(row["Mobile"].ToString());
                item.SubItems.Add(row["Gender"].ToString());
                item.SubItems.Add(Convert.ToSingle(row["Wage"]).ToString("f2"));
                item.SubItems.Add(row["CommissonRate"].ToString());
                item.SubItems.Add(row["Role"].ToString());
                this.listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = this.tb_name .Text;
            string phone = this.tb_phone .Text;
            string gender = this.cb_gender.Text;
            string Wage = this.tb_wage .Text;
            string Ticheng = this.tb_ticheng .Text;
            string juese = this.cb_juese .Text;
            if (Name == "" || phone == "" || gender == "--请选择--" || Wage == "" || Ticheng == "" || juese == "")
            {
                MessageBox.Show("员工信息填写不完整！");
                return;
            }
            string sqlStr = "";
            string flag = "";
            if (this.listView1 .SelectedItems.Count == 0)
            {
                sqlStr = string.Format(@"insert salesman([Salesman-Name],Mobile,Pwd,Gender,Wage ,CommissonRate ,Role ) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", 
                    Name, phone,"0000", gender, Wage, Ticheng, juese);
                flag = "新增";
            }
            else
            {
                string salesmanID = this.listView1 .SelectedItems[0].SubItems[0].Text;
                sqlStr = string.Format(@"update salesman set [Salesman-Name]='{0}',Mobile='{1}',Gender='{2}',Wage ='{3}',CommissonRate ='{4}',Role='{5}' where SalessmanID ='{6}'",
                     Name, phone, gender, Wage, Ticheng, juese, salesmanID);
                flag = "修改";
            }
           int rows = db.zsg(sqlStr);
            if (rows>0)
            {
                MessageBox.Show("员工" + flag + "成功！");
                //重新加载
                chaxun();
                this.tb_name .Text = "";
                this.cb_gender.Text = "--请选择--";
                this.tb_phone .Text = "";
                this.tb_wage .Text = "";
                this.tb_ticheng .Text = "";
                this.cb_juese .Text = "--请选择--";
                this.button1.Text = "新增员工";
            }
            else
            {
                MessageBox.Show("员工" + flag + "失败,请重试！");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1 .SelectedItems.Count == 0)
            {
                this.tb_name .Text = "";
                this.tb_phone .Text = "";
                this.cb_gender.Text = "--请选择--";
                this.tb_wage .Text = "";
                this.tb_ticheng .Text = "";
                this.cb_juese .Text = "--请选择--";
                this.button1.Text = "新增员工";
            }
            else
            {
                this.tb_name.Text   = this.listView1.SelectedItems[0].SubItems[1].Text;
                this.tb_phone.Text   = this.listView1.SelectedItems[0].SubItems[2].Text;
                this.cb_gender.Text  = this.listView1.SelectedItems[0].SubItems[3].Text;
                this.tb_wage .Text = this.listView1.SelectedItems[0].SubItems[4].Text;
                this.tb_ticheng.Text = this.listView1.SelectedItems[0].SubItems[5].Text;
                this.cb_juese.Text = this.listView1.SelectedItems[0].SubItems[6].Text;
                this.button1.Text = "修改员工";
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1 .SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要删除的员工！");
                return;
            }
            string salesmanID = this.listView1.SelectedItems[0].SubItems[0].Text;
            string sql = string.Format("delete Salesman where SalessmanID=" + salesmanID);
           int rows = db.zsg(sql);
            if (rows>0)
            {
                MessageBox.Show("员工删除成功！");
                chaxun();
            }
            else
                MessageBox.Show("员工删除失败！");
        }

      
    }
}
