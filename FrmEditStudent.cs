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
    public partial class FrmEditStudent : Form
    {
        public FrmEditStudent()
        {
            InitializeComponent();
        }
        private int stuId=0;
        private Action reLoad=null;
        public int pubStuId;
        public FrmEditStudent(int studentId)
        {
            InitializeComponent();
            stuId = studentId;
        }

        private void FrmEditStudent_Load(object sender, EventArgs e)
        {
            LoadClasses();
            InitStuInfo();
        }
        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //获取页面输入信息
            string stuName = txtStuName.Text.Trim();
            int classId = (int)cboClasses.SelectedValue;
            string sex = rbtMale.Checked ? rbtMale.Text.Trim() : rbtFemale.Text.Trim();
            string phone = txtPhone.Text.Trim();
            //判空处理
            if (string.IsNullOrEmpty(stuName))
            {
                MessageBox.Show("姓名不能为空", "修改学生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("电话不能为空", "修改学生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //判断数据库中是否有重复记录 姓名+电话
            string sql = "select count(1) from StudentInfo where StudentName=@StudentName and Phone=@Phone and StudentId<>@StudentId";
            SqlParameter[] paras =
            {
                new SqlParameter("@StudentName",stuName),
                new SqlParameter("@Phone",phone),
                new SqlParameter("@StudentId",stuId)
            };
            object o = SqlHelper.ExecuteScalar(sql, paras);
            if (o == null || o == DBNull.Value || ((int)o) == 0)
            {
                //修改操作
                string sqlAdd = "update StudentInfo set StudentName=@StudentName,Sex=@Sex,ClassId=@ClassId,Phone=@Phone where StudentId=@StudentId";
                SqlParameter[] paraUpdate =
                {
                    new SqlParameter("@StudentName",stuName),
                    new SqlParameter("@ClassId",classId),
                    new SqlParameter("@Sex",sex),
                    new SqlParameter("@Phone",phone),
                    new SqlParameter("@StudentId",stuId)
                };
                int count = SqlHelper.ExecuteNoQuery(sqlAdd, paraUpdate);
                if (count > 0)
                {
                    MessageBox.Show($"学生:{stuName} 修改成功", "修改学生", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //跨页面刷新，利用委托刷新
                    reLoad.Invoke();
                }
                else
                {
                    MessageBox.Show("该学生修改失败，请检查！", "修改学生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("该学生已存在！", "修改学生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //修改

        }

        private void InitStuInfo()
        {
            //获取到StudentId;
            if(this.Tag!=null)
            {
                TagObject tagObject = (TagObject)this.Tag;
                stuId = tagObject.StudentId;
                reLoad = tagObject.Reload;
            }
            //查询数据
            string sql = "select StudentName,Sex,ClassId,Phone from StudentInfo where StudentId=@StudentId";
            SqlParameter paraId = new SqlParameter("@StudentId", stuId);
            SqlDataReader dr = SqlHelper.ExecuteReader(sql, paraId);
            //读取数据,dr为一行，逐行往后读，边读边写
            if (dr.Read())
            {
                txtStuName.Text = dr["StudentName"].ToString();
                txtPhone.Text = dr["Phone"].ToString();
                string sex = dr["Sex"].ToString();
                if (sex == "男")
                {
                    rbtMale.Checked = true;
                }
                else
                {
                    rbtFemale.Checked = true;
                }
                int classId =(int) dr["ClassId"];
                cboClasses.SelectedValue = classId;

            }
            dr.Close();
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
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
