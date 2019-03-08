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
    public partial class GoodsPuInForm : Form
    {
        public GoodsPuInForm()
        {
            InitializeComponent();
        }
        //创建DBHelper对象
        DBHelper db = new DBHelper();
        //加载
        private void GoodsPuInForm_Load(object sender, EventArgs e)
        {
           

        }
        //下拉框加载
        private void xialakuang()
        {
            string sql = "select * from type where parentID=0";
            DataSet ds = db.getDataSet(sql);
            this .cb_fenlei.DataSource =ds.Tables ["info"];
            this.cb_fenlei.DisplayMember = "TypeName";

           
            this.cb_fenlei.Text = "--请选择--";
            this.cb_erji.Text = "--请选择--";
        
        }



        //一级列表选中值改变时，二级列表随之改变
        private void cb_fenlei_TextChanged(object sender, EventArgs e)
        {
            //二级分类
            //一级为鞋类
            if (this.cb_fenlei.Text == "鞋类")
            {
                this.cb_erji.Text = null;
                string sql = "select t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='鞋类'";
                DataSet ds = db.getDataSet(sql);
                DataRow dr = ds.Tables["info"].NewRow();
                dr[0] = "--请选择--";
                ds.Tables["info"].Rows.InsertAt(dr, 0);
                this.cb_erji.DataSource = ds.Tables["info"];
                this.cb_erji.DisplayMember = "TypeName";
            }
            //一级为服装
            if (this.cb_fenlei.Text == "服装")
            {
                this.cb_erji.Text = null;
                string sql = "select t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='服装'";
                DataSet ds = db.getDataSet(sql);
                DataRow dr = ds.Tables["info"].NewRow();
                dr[0] = "--请选择--";
                ds.Tables["info"].Rows.InsertAt(dr, 0);
                this.cb_erji.DataSource = ds.Tables["info"];
                this.cb_erji.DisplayMember = "TypeName";
            }
            //一级为装备
            if (this.cb_fenlei.Text == "装备")
            {
                this.cb_erji.Text = null;
                string sql = "select t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='装备'";
                DataSet ds = db.getDataSet(sql);
                DataRow dr = ds.Tables["info"].NewRow();
                dr[0] = "--请选择--";
                ds.Tables["info"].Rows.InsertAt(dr, 0);
                this.cb_erji.DataSource = ds.Tables["info"];
                this.cb_erji.DisplayMember = "TypeName";
            }
        }
        //读取信息按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //获取值
            string huohao = this.tb_huohao.Text;
            string sql = string.Format("select * from Goods ,Type where Goods .TypeID =Type .TypeID and BarCode ='{0}'",huohao);
            DataSet ds = db.getDataSet(sql);
            if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("该商品信息不存在，请添加！");
                xialakuang();
            }
            else 
            {
                this.tb_name.Text = ds.Tables[0].Rows[0][3].ToString();
                string yiji = ds.Tables[0].Rows[0][9].ToString();
                string sql2 = string.Format("select t2.TypeName , t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t1.TypeID='{0}'", yiji);
                DataSet ds2 = db.getDataSet(sql2);
                this .cb_fenlei .Text  = ds2.Tables[0].Rows[0][0].ToString();
                this.cb_erji.Text = ds2.Tables[0].Rows[0][1].ToString();
                this.tb_jinhuo.Text = ds.Tables[0].Rows[0][4].ToString();
                this.tb_linghsou.Text = ds.Tables[0].Rows[0][5].ToString();
                this.tb_zhekou.Text = ds.Tables[0].Rows[0][6].ToString();
                this.tb_kucun.Text = ds.Tables[0].Rows[0][7].ToString();
                //显示库存余量
            string shu=ds.Tables[0].Rows[0][7].ToString();
            this.xianshi.Text = "当前库存数量：" + shu;
            }
           
        
        
        }
        //清空方法
        private void clear()
        { 
           this.tb_huohao.Text=null;
           this.tb_name.Text =null;
           this.cb_fenlei.Text =null;
           this.cb_erji.Text =null;
           this.tb_jinhuo.Text =null;
           this.tb_linghsou.Text=null;
           this.tb_zhekou.Text=null;
           this.tb_kucun.Text =null;
        
        }



        //入库按钮
        private void button2_Click(object sender, EventArgs e)
        {
            //接收文本框的值
            string huohao = this.tb_huohao.Text;
            string name=  this.tb_name.Text;
            string fenlei=   this.cb_fenlei.Text;
            string fenei2 = this.cb_erji.Text;
            string jinhuo = this.tb_jinhuo.Text;
            string lingshou = this.tb_linghsou.Text;
            string zhekou = this.tb_zhekou.Text;
            string kucun = this.tb_kucun.Text;
            float result;
            //非空判断
            if (huohao == "" || name == "" || fenlei == "--请选择--" || fenei2 == "--请选择--" || jinhuo == "" || lingshou == "" || zhekou == "" || kucun == "")
            {
                MessageBox.Show("请将入库信息输入完整");
            }
            else
            {
                
                //验证折扣的文本是否为单精度数字
            if (!float .TryParse (zhekou ,out result ))
            {
                MessageBox.Show("折扣必须是数字！");
                return;
            }
            else if (result <0||result >1)
            {
                MessageBox.Show("折扣必须是0~1的数字");
                return;
            }
            string sql2 = string.Format("select  TypeID from Type where TypeName='{0}'", fenei2);
            DataSet ds2 = db.getDataSet(sql2);
            string zhi = ds2.Tables[0].Rows[0][0].ToString();

            //判断当前入库商品是否存在
            string sql = string.Format("select * from Goods where BarCode ='{0}'",huohao );
            DataSet ds = db.getDataSet(sql);
            if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
            {
                string sql1 = string.Format(@"insert into Goods (BarCode,TypeID ,GoodsName ,StorePrice ,SalePrice ,Discount ,StockNum  )
                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", huohao, zhi , name, jinhuo, lingshou, zhekou, kucun);
                int rows = db.zsg(sql1);
                if (rows > 0)
                {
                    MessageBox.Show("入库成功！");
                    clear();
                }
                else
                {
                    MessageBox.Show("入库失败！");
                }


            }
            else
            { 
             string id = ds.Tables[0].Rows[0][0].ToString();
             string sqlstr = string.Format("update Goods set StockNum =StockNum+'{0}'where GoodsID ='{1}'" ,kucun ,id );
                int rows = db.zsg(sqlstr);
                if (rows >0)
                {
                    MessageBox.Show("入库成功！");
                    //调用清空方法
                    clear();
                }
                else 
                {
                    MessageBox.Show("入库失败！");
                }
            
            }    
            }
	




        }
        //取消按钮
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
       
    }
}
