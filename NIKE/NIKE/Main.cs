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
    public partial class Main : Form
    {
        //定义IrisSkin皮肤引擎对象
        Sunisoft.IrisSkin.SkinEngine skinEngine = null;

        public Main()
        {
            InitializeComponent();
        }

        //创建DBHelper对象
        DBHelper db = new DBHelper();
        //加载
        private void Main_Load(object sender, EventArgs e)
        {
            string role = LoginInfo.RoleName.ToString();
            if (role =="导购员")
            {
                this.pictureBox5.Enabled = false;
                this.pictureBox6.Enabled = false;
                this.pictureBox7.Enabled = false;
                this.pictureBox9.Enabled = false;
                this.pictureBox11.Enabled = false;
            }

           //启动定时器
            this.timer1.Start();
            //显示欢迎
            string haoma = LoginInfo.LoginID;
            string sql = string.Format("select [Salesman-Name],Role  from Salesman where Mobile ='{0}' ", haoma);
            DataSet ds = db.getDataSet(sql);
            this.label2.Text = ds.Tables["info"].Rows[0][0].ToString() +"("+ ds.Tables["info"].Rows[0][1].ToString() +")"+ "，欢迎您！";
           

            //创建IrisSkin皮肤引擎对象
            skinEngine = new Sunisoft.IrisSkin.SkinEngine();
            //设置项目中所有窗体均应用该皮肤
            skinEngine.SkinAllForm = true;
            //加载ssk皮肤文件，当前使用的皮肤为WavaeColor1
            skinEngine.SkinFile = "界面ssk/WaveColor1.ssk";
            //绑定到下拉框中
            this.cb_skin.Text = "WaveColor1";


        }



        //timer控件Tick事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取当前时间，显示到Lable中
            this.label3.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
        }

      

        /// <summary>
        /// 向操作区域内部添加选项卡，并在选项卡页中显示窗体
        /// </summary>
        /// <param name="TabName">标签文本</param>
        /// <param name="form">需要显示的窗体</param>
        private void AddTabPage(string TabName, Form form)
        { 
           //根据标签文本，判断该选项卡页是否已经存在，避免重复
            if (TabPageIsExist(TabName))
            {
                return;
            }
        //选项卡不存在，动态创建
            //创建选项卡，设置标签文本
            TabPage page = new TabPage(TabName);
             //将窗体form添加到选项卡页
            form.TopLevel = false;//设置为非顶级控件，否则运行报错
            form.Visible = true;//设置为可见，否则窗体内容不显示
            page.Controls.Add(form);//将窗体添加至tabpage中
            //将TabPage对象添加至TabControl中
            this.tc_form.TabPages.Add(page);
            //添加后选中该选项卡页
            this.tc_form.SelectedTab = page;
        
        }


        private bool TabPageIsExist(string TabName)
        { 
        //先假设该选项卡不存在
            bool isExist = false;
            //遍历所有选项卡页，验证假设是否成立
            foreach (TabPage  page in this .tc_form .TabPages )
            {
                if (page .Text ==TabName )
                {
                    //假设不成立，更改结果
                    isExist = true;
                    //选项卡页已经存在，直接选中该选项卡
                    this.tc_form.SelectedTab = page;
                    break;
                }
            }
        //返回最终结果
            return isExist;
        
        }


        //商品入库单击事件
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //在操作区域内部添加选项卡页，显示商品入库窗体
            GoodsPuInForm goodspuinform = new GoodsPuInForm();
            AddTabPage("商品入库", goodspuinform);

        }


        //商品浏览单击事件
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            GoodsViewForm goodsviewform = new GoodsViewForm();
            AddTabPage("商品浏览",goodsviewform );
        }


        //双击标签，移除选项卡
        private void tc_form_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //获取当前选中的Tab标签
            TabPage selectedpage = this.tc_form.SelectedTab;
            //首页标签不允许移除
            if (selectedpage .Text =="首页")
            {
                return;
            }
            //移除选中的标签
            this.tc_form.TabPages.Remove(selectedpage);

        }


        //切换皮肤
        //窗体关闭事件
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要离开吗？","提示：",MessageBoxButtons .YesNo ,MessageBoxIcon .Question );
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        
        
        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cb_skin_SelectedIndexChanged(object sender, EventArgs e)
        {
            skinEngine.SkinFile = "界面ssk/" + this.cb_skin.Text + ".ssk";
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            CashForm cash = new CashForm();
            cash.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Return re = new Return();
            re.ShowDialog();
        }
        //销售统计窗体
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            //在操作区域内部添加选项卡页，显示商品入库窗体
            Sum sum = new Sum();
            AddTabPage("销售统计", sum);
          
        }
        //工资核算
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Pay pay = new Pay();
            AddTabPage("工资核算", pay);
        }
        //商品分类
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Classification classification = new Classification();
            AddTabPage("商品分类管理",classification );
        }
        //员工管理
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            AddTabPage("员工管理",employee );
        }
        //系统设置
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Windows windows = new Windows();
            windows.ShowDialog();
        }














    }
}
