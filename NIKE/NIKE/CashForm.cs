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
    public partial class CashForm : Form
    {
        public CashForm()
        {
            InitializeComponent();
        }

        //创建 DBHelper对象
        DBHelper db = new DBHelper();

        //生成小票流水号
        private void fangfa()
        {
            this.label4.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        public static float chengjiao;
       
        //加载
        private void CashForm_Load(object sender, EventArgs e)
        {
            //下拉框加载导购员
            string sql = "select distinct [Salesman-Name] from Salesman where Role ='导购员'";
            DataSet ds = db.getDataSet(sql);
            this.cb_daogou.DataSource = ds.Tables["info"];
            this.cb_daogou.DisplayMember = "Salesman-Name";
            this.cb_daogou.Text = "--请选择--";
            //加载流水号，调用方法
            fangfa();
            this.label7.Text = "收银员：" + LoginInfo.UserName;

            this.label5.Text = "共：￥0元";
            this.label6.Text = "商品数量：0";
            this.tb_yingshou.Text = "0.00";
            this.tb_shishou.Text = "0.00";
            this.tb_zhaoling.Text = "0.00";
        }



        //在条形码文本框框获得焦点时，按下键盘任意键并释放时触发的事件
        private void tb_huohao_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果按得是回车键（ASCII值为13）
            if (e.KeyChar == 13)
            {
                //获取条形码
                string barcode = this.tb_huohao.Text;
                //判断商品是否已经在购物列表中存在，存在则数量+1，不存在则添加至购物列表
                if (!IsExit(barcode))
                {
                    //购物列表中不存在该商品
                    //根据输入的条形码，查询商品信息
                    string sql = "select * from Goods ,Type where Goods .TypeID =Type .TypeID and Goods .BarCode ='" + this.tb_huohao.Text + "'";
                    DataSet ds = db.getDataSet(sql);
                    //未查询到商品，弹出提示
                    if (ds.Tables["info"].Rows.Count == 0)
                    {
                        MessageBox.Show("商品未找到，请检查条形码是否正确！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //创建购物项
                    ListViewItem item = new ListViewItem(ds.Tables["info"].Rows[0]["BarCode"].ToString());
                    item.SubItems.Add(ds.Tables["info"].Rows[0]["GoodsName"].ToString());
                    item.SubItems.Add(ds.Tables["info"].Rows[0]["TypeName"].ToString());
                    item.SubItems.Add(ds.Tables["info"].Rows[0]["SalePrice"].ToString());
                    item.SubItems.Add(ds.Tables["info"].Rows[0]["Discount"].ToString());
                    //记录商品ID
                    item.Tag = ds.Tables["info"].Rows[0]["GoodsID"].ToString();
                    //折后价=零售价*折扣
                    float saleprice = Convert.ToSingle(ds.Tables["info"].Rows[0]["SalePrice"]);
                    float discount = Convert.ToSingle(ds.Tables["info"].Rows[0]["Discount"]);
                    item.SubItems.Add((saleprice * discount).ToString("f2"));
                    //数量
                    item.SubItems.Add("1");
                    //添加至购物车列表
                    this.lv_car.Items.Add(item);
                    //计算总价
                    ShowTotalPrice();


                }
                this.tb_huohao.Text = null;
            }

        }


        //判断商品在购物列表中是否已经存在，存在则更新数量
        private bool IsExit(string barcode)
        {
            bool result = false;
            foreach (ListViewItem item in this.lv_car.Items)
            {
                if (item.Text == barcode)
                {
                    //商品在购物车中存在
                    result = true;
                    //存在，修改商品数量
                    item.SubItems[6].Text = (int.Parse(item.SubItems[6].Text) + 1).ToString();
                    break;

                }
            }
            return result;
        }

        private void lv_car_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lv_car.SelectedItems.Count == 0) return;
            this.tb_huohao.Text = this.lv_car.SelectedItems[0].Text;
        }
        //购物车获得焦点时,按下键盘某键后释放触发的事件
        private void lv_car_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果没有选中任何行,直接退出事件
            if (this.lv_car.SelectedItems.Count == 0) return;
            //获取选中行的当前数量
            int count = int.Parse(this.lv_car.SelectedItems[0].SubItems[6].Text);
            //如果选中了行,并且按下了"+(ASCII值为61)",增加数量  
            if (e.KeyChar == 61)
            {
                this.lv_car.SelectedItems[0].SubItems[6].Text = (count + 1).ToString();
            }
            //如果选中了行,并且按下了"-(ASCII值为45)",减少数量
            if (e.KeyChar == 45)
            {
                if (count == 1) return;
                this.lv_car.SelectedItems[0].SubItems[6].Text = (count - 1).ToString();
            }
            //如果选中了行,并且按下了"Backspace(ASCII值8)",从购物车中删除商品
            if (e.KeyChar == 8)
            {
                //删除前进行确认
                DialogResult result = MessageBox.Show("确定要从购物车中删除商品【" + this.lv_car.SelectedItems[0].SubItems[1] + "】?", "删除确认:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    this.lv_car.Items.Remove(this.lv_car.SelectedItems[0]);
            }
            //计算总价
            ShowTotalPrice();

        }
        //计算总价
        private void ShowTotalPrice()
        {
            

            //计算总价和总数量
            int totalCount = 0;//数量
            float totalPrice = 0;//总价
            //遍历ListView中的数据
            foreach (ListViewItem item in this.lv_car.Items)
            {
              
                totalCount += int.Parse(item.SubItems[6].Text);
                totalPrice += int.Parse(item.SubItems[6].Text) * float.Parse(item.SubItems[5].Text);

            }
            this.label5.Text = "共：￥" + totalPrice.ToString("f2") + "元";
            this.label6.Text = "商品数量：" + totalCount;
            this.tb_yingshou.Text = totalPrice.ToString("f2");
            //传值
            float money = float.Parse(this.tb_yingshou.Text);
            float pay = float.Parse(this.tb_shishou.Text);
            this.tb_zhaoling.Text = (pay - money).ToString("f2");
           

        }
        //输入实收金额后，按键盘"回车",计算找零
        private void tb_shishou_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                float money = float.Parse(this.tb_yingshou.Text);
                float pay = float.Parse(this.tb_shishou.Text);
                this.tb_zhaoling.Text = (pay - money).ToString("f2");
                if ( this.cb_daogou.Text == "--请选择--")
                {
                    MessageBox.Show("请选择该笔销售记录的导购员!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //实收金额小于应收金额
                if (pay < money )
                {
                    MessageBox.Show("付款金额不足!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                //调用函数，添加数据
                tianjia();
            }
        }

        //结算按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //接受共计的文本值
            string ReceiptsCode = this.label5.Text;
         
            //应收
           float Amount =  float.Parse(this.tb_yingshou.Text);
            //实收
            float pay = float.Parse(this.tb_shishou.Text);
            //导购员
            string SalesmanID = this.cb_daogou.SelectedValue.ToString();
        
            if (this.cb_daogou.Text == "--请选择--")
            {
                MessageBox.Show("请选择该笔销售记录的导购员!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (Amount == 0.00)
            {
                MessageBox.Show("购物车中没有商品,无法进行结算操作!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //实收金额小于应收金额
            if (pay < Amount)
            {
                MessageBox.Show("付款金额不足!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
          

            //计算找零
            float money = float.Parse(this.tb_yingshou.Text);
            pay = float.Parse(this.tb_shishou.Text);
            this.tb_zhaoling.Text = (pay - money).ToString("f2");

            //调用函数，添加数据
            tianjia();

        }

        //添加数据函数，销售明细表，销售记录表
        private void tianjia()
        {
            //向销售记录表中添加一条记录,并获取新增记录的标识列的值
            string liushuihao = this.label4.Text;
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");
            string  xiaoshoujine = this.tb_yingshou.Text;
            //导购员的id
            string sql = "select * from Salesman where [Salesman-Name] ='" + this.cb_daogou.Text + "'";
            DataSet ds = db.getDataSet(sql);
            string ID = ds.Tables[0].Rows[0][0].ToString();
            string shoyin = LoginInfo.UserID;
            //添加数据
            string sqlstr = string.Format(@"insert into Sales (ReceiptsCode ,SalesDate ,Amount ,SalesmanID ,CashierID )
            values('{0}','{1}','{2}','{3}','{4}')", liushuihao, riqi, xiaoshoujine, ID, shoyin);
            int rows=db.zsg (sqlstr);
            //获取新增记录的标识列的值
            string sqq = string .Format ("select SalesID  from Sales where  ReceiptsCode='{0}'",liushuihao);
            DataSet ds1 = db.getDataSet(sqq );
            int SaleID = int.Parse(ds1.Tables[0].Rows[0]["SalesID"].ToString());



            //遍历购物列表，将其添加至销售明细表中，并更新售出商品的库存
            string sqlStr = "";
            foreach (ListViewItem item in this.lv_car.Items)
            {
                //添加至销售明细表中
                sqlStr += string.Format(@"insert SalesDetail(SalesID,GoodsID,Quantity,Amountlone) values ('{0}','{1}','{2}',{3})", SaleID, item.Tag, item.SubItems[6].Text, item.SubItems[5].Text);
            }
            int result = db.zsg(sqlStr);
            string SQL="";
            foreach (ListViewItem item in this.lv_car.Items)
            {
                //更新售出商品的库存
                SQL += string.Format("update Goods set StockNum=StockNum-'{0}' where GoodsID='{1}'", item.SubItems[6].Text, item.Tag);
            }
            int re = db.zsg(SQL);



            if (result > 0&&re >0)
            {
                MessageBox.Show("结算成功!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //清空控件,初始化小票流水号
                this.tb_huohao.Text = "";
                this.cb_daogou.Text = "--请选择--";
                fangfa();//重新创建小票流水号
                this.lv_car.Items.Clear();
                this.label5.Text = "共：￥0.00";
                this.label6.Text = "商品数量：0";
                this.tb_yingshou.Text = "0.00";
                this.tb_shishou.Text = "0.00";
                this.tb_zhaoling.Text = "0.00";
            }
            else
            {
                MessageBox.Show("结算失败,请重试!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        
        }




        private void CashForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
       





    }
}