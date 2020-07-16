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
    public partial class FrmClassList : Form
    {
        public FrmClassList()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化年级列表，所有班级列表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmClassList_Load(object sender, EventArgs e)
        {
            InitGrades();
            InitAllClasses();
        }
        /// <summary>
        /// 加载所有班级信息
        /// </summary>
        private void InitAllClasses()
        {
            string sql= "select ClassId,ClassName,GradeName,Remark from ClassInfo c inner join GradeInfo g on c.GradeId = g.GradeId";
            DataTable dtClasses = SqlHelper.GetDataTable(sql);
            dgvClassList.DataSource = dtClasses;
        }

        /// <summary>
        /// 加载年级列表
        /// </summary>
        private void InitGrades()
        {
            string sql = "select GradeId,GradeName from GradeInfo";
            DataTable dtGradeList = SqlHelper.GetDataTable(sql);
            //添加一个  ---请选择项
            DataRow dr = dtGradeList.NewRow();
            dr["GradeId"] = 0;
            dr["GradeName"] = "请选择";
            dtGradeList.Rows.InsertAt(dr,0);
            cboGrades.DataSource = dtGradeList;
            cboGrades.DisplayMember = "GradeName";
            cboGrades.ValueMember = "GradeId";
            cboGrades.SelectedIndex = 0;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            //获取查询条件
            int gradeId =(int) cboGrades.SelectedValue;
            string className = txtClassName.Text.Trim();
            string sql = "select ClassId,ClassName,GradeName,Remark from ClassInfo c inner join GradeInfo g on c.GradeId=g.GradeId";
            sql += " where 1=1";
            if (gradeId > 0)
            {
                sql += " and c.GradeId=@GradeId";
            }
            if (!string.IsNullOrEmpty(className))
            {
                sql += " and ClassName like @ClassName";
            }
            SqlParameter[] paras =
            {
                new SqlParameter("@GradeId",gradeId),
                new SqlParameter("@ClassName","%"+className+"%")
            };
            DataTable dtClasses = SqlHelper.GetDataTable(sql,paras);
            dgvClassList.DataSource = dtClasses;
        }
    }
}
