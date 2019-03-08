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
    public partial class Sum : Form
    {
        public Sum()
        {
            InitializeComponent();
        }
        //创建 DBHelper类对象
        DBHelper db = new DBHelper();
        //加载事件
        private void Sum_Load(object sender, EventArgs e)
        {
            //加载导购员
            string sql = "select * from salesman where role='导购员'";
            DataSet ds = db.getDataSet(sql);
            DataRow dr = ds.Tables["info"].NewRow();
            dr[0] = 0;
            dr[1] = "--请选择--";
            ds.Tables["info"].Rows.InsertAt(dr,0);
            this.cb_daogou.DataSource = ds.Tables["info"];
            this.cb_daogou.DisplayMember = "Salesman-Name";
            this.cb_daogou.Text = "--请选择--";

            //加载当天销售记录
            this.cb_time .Text = "全部";

        }
        //选择日期区间，自动确定起始日期和结束日期
        private void cb_time_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            switch (this.cb_time .Text)
            {
                case "全部":
                    this.dtp_1.Enabled = false;
                    this.dtp_2.Enabled = false;
                    break;
                case "本日":
                    this.dtp_1.Enabled = true;
                    this.dtp_2.Enabled = true;
                    this.dtp_1.Value = now;
                    this.dtp_2.Value = now;
                    break;
                case "本周":
                    this.dtp_1.Enabled = true;
                    this.dtp_2.Enabled = true;
                    int week = (int)now.DayOfWeek;
                    //周日时week值为0，代表一周的第一天。
                    //由于中国人的习惯是周日为一周的最后一天，因此需要判断和转换
                    if (week == 0) week = 7;

                    this.dtp_1.Value = now.AddDays(1 - week);
                    this.dtp_2.Value = now.AddDays(7 - week);
                    break;
                case "本月":
                    this.dtp_1.Enabled = true;
                    this.dtp_2.Enabled = true;
                    int dayOfMonth = now.Day;
                    //当前月第1天日期
                    this.dtp_1.Value = now.AddDays(1 - dayOfMonth);
                    //当前月最后一天日期 = 下月第一天-1天
                    this.dtp_2.Value = now.AddDays(1 - dayOfMonth).AddMonths(1).AddDays(-1);
                    break;
                case "本年":
                    this.dtp_1.Enabled = true;
                    this.dtp_2.Enabled = true;
                    int dayOfYear = now.DayOfYear;
                    this.dtp_1.Value = now.AddDays(1 - dayOfYear);
                    this.dtp_2.Value = now.AddDays(1 - dayOfYear).AddYears(1).AddDays(-1);
                    break;
                default:
                    break;
            }
        }
        //查询按钮
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
             if (this.cb_daogou.Text == "--请选择--")
            {
                MessageBox.Show("请选择导购员！");
            }
            else
            {
                //定义变量接收文本值
                string guider = this.cb_daogou.Text;
                DateTime startDate = this.dtp_1.Value;
                DateTime endDate = this.dtp_2.Value;

                //清空列表中的值
                this.listView1.Items.Clear();

                string sql = string.Format("select * from Sales s ,Salesman a where s.SalesmanID =a.SalessmanID and [Salesman-Name] ='" + this.cb_daogou.Text + "'");
                DataSet ds = db.getDataSet(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                    {
 
                        //小票流水号
                        ListViewItem item = new ListViewItem(row["ReceiptsCode"].ToString());
                        //购物日期
                        item.SubItems.Add(((DateTime)row["SalesDate"]).ToString("yyyy-MM-dd"));
                        //购物金额
                        item.SubItems.Add(row["Amount"].ToString());
                        ////单笔利润
                        float profit = GetProfit(row["SalesID"].ToString());
                        item.SubItems.Add(profit.ToString("f2"));
                        //导购员
                        item.SubItems.Add(row["Salesman-Name"].ToString());
                        //收银员
                        string CashierName = yuangongName(row["CashierID"].ToString());
                        item.SubItems.Add(CashierName);
                        //绑定
                        this.listView1.Items.Add(item);
                    }
                  
                    this.label2.Text = "销售记录" + ds.Tables[0].Rows.Count + "条";
                    this.label3.Text = getSaleInfo();
 }
            }
            catch (Exception ex)
            {

                MessageBox.Show("运行错误！原因：" + ex.Message);
            }
           
        }

        //计算单笔利润
        private float GetProfit(string salesID)
        {   
            //单笔利润 = 该笔订单所有商品利润总和
            //商品利润 = (折后价格-进货价格)*销售数量

            string sql = "select  单笔利润 = sum((Sales.Amount-Goods.StorePrice)*SalesDetail.Quantity) from Sales, SalesDetail,Goods where SalesDetail.GoodsID=Goods.GoodsID and Sales.SalesID=" + salesID;
            float rows = Convert.ToSingle(db.ExecuteScalar(sql));
            return rows;

            
        }

        //获取收银员姓名
        private string yuangongName(string CashierID)
        {
            string sql = string.Format("select [Salesman-Name] from Salesman where SalessmanID='{0}'", CashierID);
            DataSet ds = db.getDataSet(sql);
            string name = ds.Tables[0].Rows[0][0].ToString();
            return name;
        }

        //计算总销售额和总利润
        private string getSaleInfo()
        {
            float amount = 0;
            float profit = 0;
            foreach (ListViewItem item in this.listView1.Items)
            {
                amount += float.Parse(item.SubItems[2].Text);
                profit += float.Parse(item.SubItems[3].Text);
            }
            return string.Format("销售金额￥{0}元，利润￥{1}元", amount.ToString("f2"), profit.ToString("f2"));
        }

       
    }
}
