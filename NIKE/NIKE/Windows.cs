using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NIKE
{
    public partial class Windows : Form
    {
        public Windows()
        {
            InitializeComponent();
        }
        //加载事件
        private void Windows_Load(object sender, EventArgs e)
        {
            this.tb_name.Text = Settings.ShopName;
            this.tb_pifu.Text = Settings.SkinName;
            this.tb_img.Text = Settings.AdImagePath;
            this.tb_xiaoshoue.Text = Settings.BaseSaleroom.ToString ();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tb_name.Text == "" || this.tb_pifu.Text == "" || this.tb_img.Text == "" || this.tb_xiaoshoue.Text == "")
            {
                MessageBox.Show("配置信息不能为空！");
                return;
            }
            float BaseSaleroom;
            if (float.TryParse(this.tb_xiaoshoue.Text, out BaseSaleroom))
            {
                MessageBox.Show("月度考核保底销售额必须为数字 ！");
                return;
            }

            Settings.ShopName = this.tb_name.Text;
            Settings.SkinName = this.tb_pifu.Text;
            Settings.BaseSaleroom = float.Parse(this.tb_xiaoshoue.Text);
            Settings.AdImagePath = this.tb_img.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
