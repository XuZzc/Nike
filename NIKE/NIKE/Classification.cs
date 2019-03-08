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
    public partial class Classification : Form
    {
        public Classification()
        {
            InitializeComponent();
        }

        //创建DBhelper类
        DBHelper db =new DBHelper();


        //加载事件
        private void Classification_Load(object sender, EventArgs e)
        {
            Types();
            //加载父级分类列表
            this.cb_type.ValueMember = "TypeID";
            this.cb_type.DisplayMember = "TypeName";
            string sqlStr = string.Format("select * from type where parentID=0");
            DataSet ds1 = db.getDataSet(sqlStr );
            DataTable dt1 = ds1.Tables[0];
            //添加新行
            DataRow newRow = dt1.NewRow();
            newRow["TypeID"] = 0;
            newRow["TypeName"] = "无";
            dt1.Rows.Add(newRow);
            this.cb_type.DataSource = dt1;
            this.cb_type.Text = "--请选择--";

        }
        //查询商品分类
        public void  Types()
        {
            //清空列表
            this.listView1.Items.Clear();
            //查询商品分类表
            string sql = "select * from Type ";
            DataSet ds = db.getDataSet(sql);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["TypeID"].ToString());
                item.SubItems.Add(row["TypeName"].ToString());
                string parentTypeName = GetTypeName(row["ParentID"].ToString());
                item.SubItems.Add(parentTypeName);
                item.Tag = row["ParentID"].ToString();  //Tag中保存ParentID
                this.listView1.Items.Add(item);
            }
        }



        //定义函数获取TypeName
        private string GetTypeName(string typeID)
        {
            if (typeID == "0")
                return "无";
            else
            {
                string sql = "select TypeName from Type where typeID=" + typeID;
                object obj = db.ExecuteScalar(sql);
                return obj.ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //接收文本值
            string typeName = this.tb_name.Text;
            string parentTypeID = this.cb_type .SelectedValue.ToString();
            if (typeName == "")
            {
                MessageBox.Show("商品分类名称不能为空！");
                return;
            }
            if (this.cb_type .Text == "--请选择--")
            {
                MessageBox.Show("请选择父级分类！");
                return;
            }
            string sqlStr = "";
            string flagStr = "";
            if (this.listView1.SelectedItems.Count == 0)
            {
                //未选中一行，新增商品分类
                sqlStr = string.Format("insert type(TypeName, ParentID) values('{0}',{1})", typeName, parentTypeID);
                flagStr = "新增";
            }
            else
            {
                //选中了一行，编辑该商品分类
                string typeID = this.listView1 .SelectedItems[0].SubItems[0].Text;
                sqlStr = string.Format("update type set TypeName='{0}',ParentID={1} where TypeID={2}", typeName, parentTypeID, typeID);
                flagStr = "修改";
            }
            int rows=db.zsg(sqlStr);

            if (rows > 0)
            {
                MessageBox.Show("商品分类" + flagStr + "成功！");
                this.listView1.Items.Clear();
                //查询商品分类表
                Types();
            }
            else
            {
                MessageBox.Show("商品分类" + flagStr + "失败，请重试！");
            }
        }
        //如果选中行，则填充分类信息，否则清空
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                //未选中一行， 清空输入
                this.button1.Text = "新增分类";
                this.tb_name .Text = "";
                this.cb_type .Text = "--请选择--";
            }
            else
            {
                //如果选中了一行，填充分类信息
                this.button1.Text = "修改分类";
                this.tb_name.Text = this.listView1.SelectedItems[0].SubItems[1].Text;
                this.cb_type.Text = "";
                this.cb_type.SelectedText = this.listView1.SelectedItems[0].SubItems[2].Text;
                this.cb_type.SelectedValue = this.listView1.SelectedItems[0].Tag.ToString();
            }     
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.listView1 .SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要删除的商品分类！");
                return;
            }
            string typeID = this.listView1.SelectedItems[0].SubItems[0].Text;
            string sql = string.Format("delete type where TypeID=" + typeID);
            int rr = db.zsg(sql);
            if (rr>0)
            {
                   MessageBox.Show("商品分类删除成功！");
                   Types();
            }
            else
                MessageBox.Show("商品分类删除失败！");
        }





    }
}
