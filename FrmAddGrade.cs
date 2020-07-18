using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinStudent
{
    public partial class FrmAddGrade : Form
    {
        public FrmAddGrade()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加年级功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGrade_Click(object sender, EventArgs e)
        {
            //获取参数信息
            string gradeName = txtGradeName.Text.Trim();
            //判读是否为空
            if (string.IsNullOrEmpty(gradeName))
            {
                MessageBox.Show("年级名称不能为空", "添加年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //判断数据库表中是否有重复记录
            string sqlExits = "select count(1) from GradeInfo where GradeName=@GradeName";
            SqlParameter[] paras =
            {
                new SqlParameter("@GradeName",gradeName)
            };
            object count = SqlHelper.ExecuteScalar(sqlExits, paras);
            if(count==null || count==DBNull.Value || ((int)count == 0)){
                //添加年级操作
                string sqlAdd = "insert into GradeInfo (GradeName) values (@GradeName)";
                SqlParameter[] paraAdd =
                {
                    new SqlParameter("@GradeName",gradeName)
                };
                int countAdd = SqlHelper.ExecuteNoQuery(sqlAdd, paraAdd);
                if (countAdd > 0)
                {
                    MessageBox.Show($"年级：{gradeName} 添加成功", "添加班级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加失败", "添加年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("该年级已存在，无法添加", "添加年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
