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
    public partial class FrmAddStudent : Form
    {
        public FrmAddStudent()
        {
            InitializeComponent();
        }

        private void FrmAddStudent_Load(object sender, EventArgs e)
        {
            //加载班级列表，性别默认男
            InitClasses();
            rbtMale.Checked = true;
        }

        private void InitClasses()
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
            
            cboClasses.DataSource = dtClasses;
            cboClasses.DisplayMember = "ClassName";
            cboClasses.ValueMember = "ClassId";
            cboClasses.SelectedIndex = 0;
        }
        /// <summary>
        /// 添加学生信息功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //获取页面信息输入
            string stuName = txtStuName.Text.Trim();
            int classId = (int)cboClasses.SelectedValue;
            string sex = rbtMale.Checked ? rbtMale.Text.Trim() : rbtFemale.Text.Trim();
            string phone = txtPhone.Text.Trim();
            //判断是否为空
            if (string.IsNullOrEmpty(stuName))
            {
                MessageBox.Show("姓名不能为空", "添加学生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("电话不能为空", "添加学生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //判断姓名+电话是否再数据库中存在
            string sql = "select count(1) from StudentInfo where StudentName=@StudentName and Phone=@Phone";
            SqlParameter[] paras =
            {
                new SqlParameter(@"StudentName",stuName),
                new SqlParameter(@"Phone",phone)
            };
            object o = SqlHelper.ExecuteScalar(sql, paras);
            if(o==null || o==DBNull.Value || ((int)o) == 0)
            {
                //插入操作
                string sqlAdd = "insert into StudentInfo (StudentName,ClassId,Sex,Phone) values (@StudentName,@ClassId,@Sex,@Phone)";
                SqlParameter[] paraAdd =
                {
                    new SqlParameter("@StudentName",stuName),
                    new SqlParameter("@ClassId",classId),
                    new SqlParameter("@Sex",sex),
                    new SqlParameter("@Phone",phone)
                };
                int count = SqlHelper.ExecuteNoQuery(sqlAdd, paraAdd);
                if (count > 0)
                {
                    MessageBox.Show($"学生:{stuName} 添加成功", "添加学生", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("该学生添加失败，请假查！", "添加学生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("该学生已存在！", "添加学生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
