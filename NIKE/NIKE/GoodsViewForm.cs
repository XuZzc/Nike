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
    public partial class GoodsViewForm : Form
    {
        public GoodsViewForm()
        {
            InitializeComponent();
        }
        //创建DBHelper对象
        DBHelper db = new DBHelper();

        private void one()
        { 
          //一级分类
            string sql = "select * from type where parentID=0";
            DataSet ds = db.getDataSet(sql);
            DataRow dr = ds.Tables["info"].NewRow();
            dr[0] = 0;
            dr[1] = "全部";
            ds.Tables["info"].Rows.InsertAt(dr,0);
            this.cb_yijifenlei .DataSource =ds.Tables ["info"];
            this.cb_yijifenlei.DisplayMember = "TypeName";
           
        }
        //加载事件
        private void GoodsViewForm_Load(object sender, EventArgs e)
        {
            //一级分类
            one();
            this.cb_yijifenlei.Text = "全部";
            //时间区域
            this.cb_time.Text = "全部";
        }
      
        //联动效果：一级列表选中值改变时，二级列表随之改变
        private void cb_yijifenlei_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (this.cb_yijifenlei .Text=="全部")
            {
                this.cb_erjifenlei.Text = "全部";
                return;
            }
            //一级为鞋类
            if (this.cb_yijifenlei .Text == "鞋类")
            {
                this.cb_erjifenlei.Text = null;
                string sql = "select t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='鞋类'";
                DataSet ds = db.getDataSet(sql);
                DataRow dr = ds.Tables["info"].NewRow();
                dr[0] = "--请选择--";
                ds.Tables["info"].Rows.InsertAt(dr, 0);
                this.cb_erjifenlei.DataSource = ds.Tables["info"];
                this.cb_erjifenlei.DisplayMember = "TypeName";
            }
            //一级为服装
            if (this.cb_yijifenlei .Text == "服装")
            {
                this.cb_erjifenlei.Text = null;
                string sql = "select t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='服装'";
                DataSet ds = db.getDataSet(sql);
                DataRow dr = ds.Tables["info"].NewRow();
                dr[0] = "--请选择--";
                ds.Tables["info"].Rows.InsertAt(dr, 0);
                this.cb_erjifenlei.DataSource = ds.Tables["info"];
                this.cb_erjifenlei.DisplayMember = "TypeName";
            }
            //一级为装备
            if (this.cb_yijifenlei .Text == "装备")
            {
                this.cb_erjifenlei.Text = null;
                string sql = "select t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='装备'";
                DataSet ds = db.getDataSet(sql);
                DataRow dr = ds.Tables["info"].NewRow();
                dr[0] = "--请选择--";
                ds.Tables["info"].Rows.InsertAt(dr, 0);
                this.cb_erjifenlei.DataSource = ds.Tables["info"];
                this.cb_erjifenlei.DisplayMember = "TypeName";
            }
           
        }
     

        //时间的确定
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取系统当前时间
            DateTime now = DateTime.Now;
            //获取今天是本月的第几天
            int dayofmonth = now.Day;
            switch (this.cb_time.Text)
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
                    //获取今天是本周的第几天
                    int dayofweek = (int)now.DayOfWeek;
                    //周日时dayofweek值为0，代表一周的第一天
                    if (dayofweek == 0)
                    {
                        dayofweek = 7;
                    }
                    //本周第一天日期=今天日期-dayofweek+1天
                    this.dtp_1.Value = now.AddDays(-dayofweek + 1);
                    //本周最后一天日期=本周第一天日期+6
                    this.dtp_2.Value = now.AddDays(-dayofweek + 1 + 6);
                    break;
                case "本月":
                     this.dtp_1.Enabled = true;
                     this.dtp_2.Enabled = true;
                    this.dtp_1.Value = now.AddDays(-dayofmonth + 1);
                    this.dtp_2.Value = now.AddDays(-dayofmonth + 1).AddMonths(1).AddDays(-1);
                    break;
                case "上月":
                      this.dtp_1.Enabled = true;
                     this.dtp_2.Enabled = true;
                    this.dtp_1.Value = now.AddDays(-dayofmonth + 1).AddMonths(-1);
                    this.dtp_2.Value = now.AddDays(-dayofmonth + 1).AddDays(-1);
                    break;
                case "本年":
                      this.dtp_1.Enabled = true;
                     this.dtp_2.Enabled = true;
                    int dayofyear = now.DayOfYear;
                    this.dtp_1.Value = now.AddDays(-dayofyear + 1);
                    this.dtp_2.Value = now.AddDays(-dayofyear + 1).AddYears(1).AddDays(-1);
                    break;
                default:
                    break;
            }

        }

        //查询函数
        private void QueryGoods()
        {
            string goodsCode = this.tb_huohao.Text;
            string goodsName = this.tb_name.Text;
            string timename=this.cb_time .Text;
            string yijikuang = this.cb_yijifenlei.Text;
            string erjikuang = this.cb_erjifenlei.Text;
            DateTime startTime = dtp_1.Value;
            DateTime endTime = dtp_2.Value;
            //条形码、名字为空，其他为全部的情况下，查询所有
            if (goodsCode ==""&&goodsName ==""&&timename =="全部"&&yijikuang =="全部"&&erjikuang =="全部" )
            {
                string sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID";
                DataSet ds = db.getDataSet(sqlstr);
                this.dataGridView1.DataSource = ds.Tables["info"];
                 if (ds.Tables .Count ==1&&ds.Tables [0].Rows .Count ==0)
                 {
                     MessageBox.Show("没有相关联的信息！");
                 }
            }
            //条形码不为空，其他不变
            if (goodsCode !="")
            { 
              string sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID and BarCode like '" + this.tb_huohao.Text + "%'";
              DataSet ds = db.getDataSet(sqlstr);
              this.dataGridView1.DataSource = ds.Tables["info"];
              if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
              {
                  MessageBox.Show("没有相关联的信息！");
              }
            }
            //名字不为空，其他不变
           if (goodsName !="")
            {
                string sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID and GoodsName like '%" + goodsName  + "%'";
                DataSet ds = db.getDataSet(sqlstr);
                this.dataGridView1.DataSource = ds.Tables["info"];
                if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("没有相关联的信息！");
                }
            }
            //时间不为空
           if (timename !="全部")
            {
                string sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID and StockDate between '" + startTime.ToString("yyyy-MM-dd 00:00:00") + "' and '" + endTime.ToString("yyyy-MM-dd 23:59:59") + "'";
                DataSet ds = db.getDataSet(sqlstr);
                this.dataGridView1.DataSource = ds.Tables["info"];
                if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("没有相关联的信息！");
                }
           }

            //问题
           // //一级分类和二级分类不等于全部
           //if (yijikuang !="全部"&&erjikuang =="--请选择--")
           //{
           //    string parentID = GetTypeID(this.cb_yijifenlei.Text);
           //    string  sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID  and ParentID=" + parentID;
           //    DataSet ds = db.getDataSet(sqlstr);
           //    this.dataGridView1.DataSource = ds.Tables["info"];
           //    if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
           //    {
           //        MessageBox.Show("没有相关联的信息！");
           //    }
           //}
           //if ( yijikuang != "全部" && erjikuang != "--请选择--")
           //{
           //     string parentID = GetTypeID(this.cb_yijifenlei.Text);
           //    string  sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID  and ParentID="+ parentID+" and TypeName ='"+ this.cb_erjifenlei .Text +"'"  ;
           //    DataSet ds = db.getDataSet(sqlstr);
           //    this.dataGridView1.DataSource = ds.Tables["info"];
           //    if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
           //    {
           //        MessageBox.Show("没有相关联的信息！");
           //    }
           //}
            //时间不为全部，货号不为空 ，名字不不为空
           if (timename !="全部"&&goodsCode !=""||timename !="全部"&&goodsName !="")
           {
               if (timename !="全部"&&goodsCode !="")
               {
                string sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID and StockDate between '" + startTime.ToString("yyyy-MM-dd 00:00:00") + "' and '" + endTime.ToString("yyyy-MM-dd 23:59:59") + "'and BarCode like '" + this.tb_huohao.Text + "%'";
                DataSet ds = db.getDataSet(sqlstr);
                this.dataGridView1.DataSource = ds.Tables["info"];
                if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("没有相关联的信息！");
                }
               }
               if (timename !="全部"&&goodsName !="")
               {
                string sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID and StockDate between '" + startTime.ToString("yyyy-MM-dd 00:00:00") + "' and '" + endTime.ToString("yyyy-MM-dd 23:59:59") + "'and GoodsName like '%" + goodsName  + "%'";
                DataSet ds = db.getDataSet(sqlstr);
                this.dataGridView1.DataSource = ds.Tables["info"];
                if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("没有相关联的信息！");
                }
               }


           }
            //时间不为全部，一级分类不为全部，二级分类不为--请选择--
           //if (timename != "全部" && yijikuang != "全部" && erjikuang == "--请选择--" || timename != "全部" && yijikuang != "全部" && erjikuang != "--请选择--")
           //{
           //    if (timename != "全部" && yijikuang != "全部" && erjikuang == "--请选择--")
           //    {
           //     string parentID = GetTypeID(this.cb_yijifenlei.Text);
           //     string sql = "select * from Goods,Type where  goods.TypeId=Type.TypeID and StockDate between '" + startTime.ToString("yyyy-MM-dd 00:00:00") + "' and '" + endTime.ToString("yyyy-MM-dd 23:59:59") + "'and ParentID="+ parentID;
           //     DataSet ds = db.getDataSet(sql);
           //     this.dataGridView1.DataSource = ds.Tables["info"];
           //     if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
           //     {
           //         MessageBox.Show("没有相关联的信息！");
           //     }
           //    }
           //    if (timename != "全部" && yijikuang != "全部" && erjikuang != "--请选择--")
           //    {
           //        string parentID = GetTypeID(this.cb_yijifenlei.Text);
           //        string sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID and StockDate between '" + startTime.ToString("yyyy-MM-dd 00:00:00") + "' and '" + endTime.ToString("yyyy-MM-dd 23:59:59") + "'and ParentID="+ parentID+" and TypeName ='"+ this.cb_erjifenlei .Text +"'";
           //        DataSet ds = db.getDataSet(sqlstr);
           //        this.dataGridView1.DataSource = ds.Tables["info"];
           //        if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
           //        {
           //            MessageBox.Show("没有相关联的信息！");
           //        }
               
               
           //    }


           //}







            ////定义查询所有商品的sqlF 
            //string sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID";
            ////根据用户的选择，拼接不同的条件
            //if (goodsCode != "")
            //{
            //    sqlstr = "select * from Goods,Type where  goods.TypeId=Type.TypeID and BarCode like '" + this.tb_huohao.Text + "%'";
            //}
            //if (goodsName != "")
            //{
            //    sqlstr += "and GoodsName like '%" + goodsName  + "%'";
            //}
            //if (typeName != "全部")
            //{
            //    //用户选择了二级分类，按二级分类查询
            //    sqlstr += " and TypeName='" + typeName  + "'";
            //}
            //else if (this.cb_yijifenlei.Text != "全部")
            //{
            //   //用户只选择一级分类，按一级分类查询
            //    string parentID = GetTypeID(this.cb_yijifenlei.Text);
            //    sqlstr += "and ParentID=" + parentID;

            //}
          
            //if (this.cb_time.Text != "全部")
            //    sqlstr += "and StockDate between '" + startTime.ToString("yyyy-MM-dd 00:00:00") + "' and '" + endTime.ToString("yyyy-MM-dd 23:59:59") + "'";
            //    //按入库时间降序排列
            //    sqlstr += "order by StockDate desc";
            //    //执行sql
            //    DataSet ds = db.getDataSet(sqlstr);

            //    if (ds.Tables .Count ==1&&ds.Tables [0].Rows .Count ==0)
            //    {
            //        MessageBox.Show("没有相关联的信息！");
            //    }
            //    //绑定至dataGridView1 
            //    this.dataGridView1.AutoGenerateColumns = false;
            //    this.dataGridView1.DataSource = ds.Tables["info"];
            //    this.lb_yuju.Text = "当前共" + ds.Tables["info"].Rows.Count + "条商品信息！";

        }
        //根据TypeName查询TypeID
        private string GetTypeID(string TypeName)
        {
            string sqlStr = "select TypeID from type where TypeName='" + TypeName + "'";
            DataSet ds = db.getDataSet(sqlStr);
            return ds.Tables[0].Rows[0].ToString();
        }
        //查询按钮
        private void button1_Click(object sender, EventArgs e)
        {

            //接收文本值
            string goodsCode = this.tb_huohao.Text;
            string goodsName = this.tb_name.Text;
            string yijikuang = this.cb_yijifenlei.Text;
            string erjikuang = this.cb_erjifenlei.Text;
            DateTime startTime = dtp_1.Value;
            DateTime endTime = dtp_2.Value;
            //调用多条件查询函数
            QueryGoods();
            chongzhi();
        }
        private void chongzhi()
        {
            this.tb_huohao.Text = null;
            this.tb_name.Text = null;
            this.cb_time.Text = "全部";
            this.cb_yijifenlei.Text = "全部";
            this.cb_erjifenlei.Text = "全部";
        }





        private void cb_yijifenlei_TextChanged(object sender, EventArgs e)
        {

        }

    
       



    }
}
