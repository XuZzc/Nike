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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        //创建 DBHelper类
        DBHelper db = new DBHelper();
        //加载
        private void Return_Load(object sender, EventArgs e)
        {
            this.label4.Text = "0.00";
            this.label5.Text = "0.00";
        }
        //查询按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //接收流水号
            string liushuihao = this.tb_huohao.Text;
            if (liushuihao == "")
            {
                MessageBox.Show("请输入流水号");
            }
            else { 

            string sql = string.Format("select * from Sales,SalesDetail ,Goods,Salesman where Sales .SalesID =SalesDetail .SalesID and SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Sales .ReceiptsCode='{0}'", liushuihao);
            DataSet ds = db.getDataSet(sql);
            if (ds.Tables .Count ==1&&ds.Tables[0].Rows .Count ==0)
            {
                MessageBox.Show("没有此流水号的交易记录！");
            }
            //绑定数据
            this.dataGridView1.AutoGenerateColumns = false;
            this .dataGridView1 .DataSource =ds.Tables ["info"];
            //显示交易金额
            float sum = 0;
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                sum += Convert.ToSingle(row.Cells[2].Value) * Convert.ToInt32(row.Cells[3].Value);
            }

            this.label4.Text = sum.ToString("f2");
          }

        }
        //单击时触发
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            

            //退款金额
            float sum = 0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                sum += Convert.ToSingle(row.Cells[2].Value) * Convert.ToInt32(row.Cells[3].Value);
            }

            this.label5.Text = sum.ToString("f2");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        //退货按钮
        private void button2_Click(object sender, EventArgs e)
        {
            //退货金额  
            float returnMoney = float.Parse(this.label5.Text);
            //订单交易金额
            float totalMoney = float.Parse(this.label4 .Text );
            if (returnMoney ==0)
            {
                MessageBox.Show("请选择需要退货的商品！","提示：",MessageBoxButtons .OK ,MessageBoxIcon .Warning );
                return;

            }
            string sqlstr = "";
            //获取salesID
            string huohao = this.tb_huohao.Text;
            string sql = string.Format("select * from Sales,SalesDetail where Sales .SalesID =SalesDetail .SalesID and ReceiptsCode='"+ huohao+"'");
            DataSet ds = db.getDataSet(sql);
            string salesID = ds.Tables[0].Rows[0][0].ToString();

            //判断是“全退”还是“部分退”
            if (returnMoney == totalMoney)
            {
                //如果是全退，则删除销售记录和销售明细
                sqlstr = string.Format("delete SalesDetail where SalesID ='{0}'  delete Sales where SalesID ='{0}'", salesID);
              
            }
            else
            {
                //如果部分退，则删除退货商品的销售明细，修改销售记录中的交易金额
                foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
                {
                    //获取当前销售明细ID

                    string sqls = string.Format("select * from Sales,SalesDetail where Sales .SalesID =SalesDetail .SalesID and ReceiptsCode='" + this.tb_huohao.Text+ "'");
                    DataSet ds1 = db.getDataSet(sql);
                    string SDID = ds.Tables[0].Rows[0]["SDID"].ToString();
                  
                    sqlstr += string.Format("delete SalesDetail where SDID='{0}'", SDID);

                }
                sqlstr += string.Format("update Sales set Amount =Amount-{0} where SalesID ={1}", returnMoney, salesID);

            }
            //执行sql
            int rows = db.zsg(sqlstr);
            if (rows > 0)
            {
                MessageBox.Show("退货成功！");
                //重新绑定datagridview
                string liushuihao = this.tb_huohao.Text;
                string sql1 = string.Format("select * from Sales,SalesDetail ,Goods,Salesman where Sales .SalesID =SalesDetail .SalesID and SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Sales .ReceiptsCode ='{0}'", liushuihao);
                DataSet ds1 = db.getDataSet(sql1);
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = ds1.Tables["info"];
                float sum = 0;
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    sum += Convert.ToSingle(row.Cells[2].Value) * Convert.ToInt32(row.Cells[3].Value);
                }

                this.label4.Text = sum.ToString("f2");
            }
            else
            {
                MessageBox.Show("退货失败，请重试！");
            }








        }
        //取消按钮
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

       

        
    }
}
