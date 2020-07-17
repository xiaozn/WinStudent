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
    public partial class FrmAddClass : Form
    {
        public FrmAddClass()
        {
            InitializeComponent();
        }

        private void FrmAddClass_Load(object sender, EventArgs e)
        {
            //加载年级列表
            InitGradeList();
        }

        private void InitGradeList()
        {
            string sql = "select GradeId,GradeName from GradeInfo";
            DataTable dtGradeList = SqlHelper.GetDataTable(sql);
            //DataRow dr = dtGradeList.NewRow();
            //dr["GradeId"] = 0;
            //dr["GradeName"] = "请选择";
            //dtGradeList.Rows.InsertAt(dr, 0);
            cboGrades.DataSource = dtGradeList;
            cboGrades.DisplayMember = "GradeName";
            cboGrades.ValueMember = "GradeId";
            cboGrades.SelectedIndex = 0;
        }
        /// <summary>
        /// 添加班级信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //信息获取
            string className = txtClassName.Text.Trim();
            int gradeId = (int)cboGrades.SelectedValue;
            string remark = txtRemark.Text.Trim();
            //判读是否为空
            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("班级名称不能为空！","添加班级提醒", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //判断数据库是否已存在该班级
            {
                string sqlExists = "select count(1) from ClassInfo where ClassName=@ClassName and GradeId=@GradeId";
                SqlParameter[] paras =
                {
                    new SqlParameter("@ClassName",className),
                    new SqlParameter("@GradeId",gradeId)
                };
                object oCount = SqlHelper.ExecuteScalar(sqlExists, paras);
                if(oCount==null || oCount==DBNull.Value || ((int)oCount) == 0)
                {
                    //添加操作

                }
                else
                {
                    MessageBox.Show("该班级名称已存在", "添加班级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }
    }
}
