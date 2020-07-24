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
    public partial class FrmEditClass : Form
    {
        private int classId;
        private Action reLoad=null;
        public FrmEditClass()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 打开页面，加载年级列表，加载班级信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmEditClass_Load(object sender, EventArgs e)
        {
            InitGradeList();
            InitClassInfo();
        }

        private void InitGradeList()
        {
            string sql = "select GradeId,GradeName from GradeInfo";
            DataTable dtGradeList = SqlHelper.GetDataTable(sql);
            cboGrades.DataSource = dtGradeList;
            cboGrades.DisplayMember = "GradeName";
            cboGrades.ValueMember = "GradeId";
        }

        private void InitClassInfo()
        {
            if (this.Tag != null)
            {
                TagObject tagObject = (TagObject)this.Tag;
                classId = tagObject.EditId;
                reLoad = tagObject.Reload;

            }
            string sql = "select ClassName,GradeId,Remark from ClassInfo where ClassId=@ClassId";
            SqlParameter paraId = new SqlParameter("@ClassId", classId);
            SqlDataReader dr = SqlHelper.ExecuteReader(sql, paraId);
            if (dr.Read())
            {
                txtClassName.Text = dr["ClassName"].ToString();
                txtRemark.Text = dr["Remark"].ToString();
                int gradeId = (int)dr["GradeId"];
                cboGrades.SelectedValue = gradeId;
            }
            dr.Close();
        }
        /// <summary>
        /// 提交修改信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}
