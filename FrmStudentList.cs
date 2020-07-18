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
    public partial class FrmStudentList : Form
    {
        public FrmStudentList()
        {
            InitializeComponent();
        }
        //实现单例
        private static FrmStudentList frmStudentList = null;
        public static FrmStudentList CreateInstance()
        {
            if (frmStudentList == null || frmStudentList.IsDisposed)
                frmStudentList = new FrmStudentList();
            return frmStudentList;
        }
        //加载班级列表，加载所有的学生信息
        private void FrmStudentList_Load(object sender, EventArgs e)
        {
            //加载班级列表
            LoadClasses();
            //加载所有学生信息
            LoadAllStudentList();
        }

        private void LoadAllStudentList()
        {
            string sql = "select StudentId,StudentName,ClassName,GradeName,Sex,Phone from StudentInfo s " + "inner join ClassInfo c on c.ClassId=s.ClassId "+"inner join GradeInfo g on g.GradeId=c.GradeId";
            //加载数据
            DataTable dtStudents = SqlHelper.GetDataTable(sql);
            //数据的组装
            if (dtStudents.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dtStudents.Rows)
                {
                    string className = dataRow["ClassName"].ToString();
                    string gradeName = dataRow["GradeName"].ToString();
                    dataRow["ClassName"] = className + " -- " + gradeName;
                }
            }
            //只想显示固定的列
            dgvStudents.AutoGenerateColumns = false;
            //绑定数据
            dgvStudents.DataSource = dtStudents;
            
        }

        private void LoadClasses()
        {
            string sql = "select ClassId,ClassName,GradeName from ClassInfo c ,GradeInfo g where c.GradeId=g.GradeId";
            DataTable dtClasses = SqlHelper.GetDataTable(sql);
            //班级年级组合项添加
            if (dtClasses.Rows.Count > 0)
            {
                foreach (DataRow dr in dtClasses.Rows)
                {
                    string className = dr["ClassName"].ToString();
                    string gradeName = dr["GradeName"].ToString();
                    dr["ClassName"] = className + " -- " + gradeName;
                }
            }
            //添加默认选择项
            DataRow drNew = dtClasses.NewRow();
            drNew["ClassId"] = 0;
            drNew["ClassName"] = "请选择";
            dtClasses.Rows.InsertAt(drNew, 0);
            cboClasses.DataSource = dtClasses;
            cboClasses.DisplayMember = "ClassName";
            cboClasses.ValueMember = "ClassId";
            cboClasses.SelectedIndex = 0;
        }
        /// <summary>
        /// 查询学生信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            //接收条件设置信息
            int classId =(int) cboClasses.SelectedValue;
            string stuName = txtStuName.Text.Trim();


            string sql = "select StudentId,StudentName,ClassName,GradeName,Sex,Phone from StudentInfo s " + "inner join ClassInfo c on c.ClassId=s.ClassId " + "inner join GradeInfo g on g.GradeId=c.GradeId";
            sql += " where 1=1";
            if (classId > 0)
            {
                sql += " and s.ClassId=@ClassId";
            }
            if (!string.IsNullOrEmpty(stuName))
            {
                sql += " and StudentName like @StudentName";
            }
            sql += " order by StudentId";
            SqlParameter[] paras =
            {
                new SqlParameter(@"ClassId",classId),
                new SqlParameter(@"StudentName","%"+stuName+"%")
            };
            
            //加载数据
            DataTable dtStudents = SqlHelper.GetDataTable(sql,paras);
            //数据的组装
            if (dtStudents.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dtStudents.Rows)
                {
                    string className = dataRow["ClassName"].ToString();
                    string gradeName = dataRow["GradeName"].ToString();
                    dataRow["ClassName"] = className + " -- " + gradeName;
                }
            }
            //只想显示固定的列
            dgvStudents.AutoGenerateColumns = false;
            //绑定数据
            dgvStudents.DataSource = dtStudents;
        }
    }
}
