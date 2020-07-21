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
        //内置委托 action不带返回值，可以不带参数，带参数最多16个
        //func带一个返回值，可以不带参数，单参数最多16个
        public Action reLoad = null;
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
            string sql = "select StudentId,StudentName,ClassName,GradeName,Sex,Phone from StudentInfo s " + "inner join ClassInfo c on c.ClassId=s.ClassId "+"inner join GradeInfo g on g.GradeId=c.GradeId"+" where s.IsDeleted=0";
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


            string sql = "select StudentId,StudentName,ClassName,GradeName,Sex,Phone from StudentInfo s " + "inner join ClassInfo c on c.ClassId=s.ClassId " + "inner join GradeInfo g on g.GradeId=c.GradeId"+" where s.IsDeleted=0";
            sql += " where 1=1";
            if (classId > 0)
            {
                sql += " and s.ClassId=@ClassId";
            }
            if (!string.IsNullOrEmpty(stuName))
            {
                sql += " and StudentName like @StudentName";
            }
            sql += " where s.IsDeleted=0";
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
        /// <summary>
        /// 修改或删除功能实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataRow dr = (dgvStudents.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                //获取点击的单元格
                DataGridViewCell cell = dgvStudents.Rows[e.RowIndex].Cells[e.ColumnIndex];

                //判断是修改还是删除
                if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString() == "修改")
                {
                    //修改操作，打开修改页面，并把StudentId传过去
                    //传值：1.构造函数  2.Tag属性（推荐）  3.公共参数
                    reLoad = LoadAllStudentList;//赋值给委托
                    int stuId = (int)dr["StudentId"];
                    //1.构造函数
                    FrmEditStudent frmEditStudent = new FrmEditStudent(stuId);
                    //2.tag属性
                    frmEditStudent.Tag = new TagObject() {
                        EditId = stuId,
                        Reload = reLoad
                    };
                   
                    //3.公共参数
                    frmEditStudent.pubStuId = stuId;
                    frmEditStudent.MdiParent = this.MdiParent;
                    frmEditStudent.Show();
                }
                else if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString() == "删除")
                {
                    
                    int stuId = int.Parse(dr["StudentId"].ToString());
                    string stuName = dr["StudentName"].ToString();
                    //删除操作
                    if (MessageBox.Show($"确定删除学生：{stuName} 的信息吗？", "删除学生提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        //假删除 isdeleted  0--->1
                        string sqlDel0 = "update StudentInfo set IsDeleted=1 where StudentId=@StudentId";
                        SqlParameter para = new SqlParameter("@StudentId", stuId);
                        int count = SqlHelper.ExecuteNoQuery(sqlDel0, para);
                        if (count > 0)
                        {
                            MessageBox.Show("该学生信息删除成功", "删除学生信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //手动刷新datagridview
                            DataTable dtStudents = (DataTable)dgvStudents.DataSource;
                            dgvStudents.DataSource = null;
                            dtStudents.Rows.Remove(dr);
                            dgvStudents.DataSource = dtStudents;
                        }
                        else
                        {
                            MessageBox.Show("学生信息删除失败", "删除提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //真删除
                        //string sqlDel0 = "delete StudentInfo where StudentId=@StudentId";
                        //SqlParameter para = new SqlParameter("@StudentId", stuId);
                        //int count = SqlHelper.ExecuteNoQuery(sqlDel0, para);
                        //if (count > 0)
                        //{
                        //    MessageBox.Show("该学生信息删除成功", "删除学生信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    //手动刷新datagridview
                        //    DataTable dtStudents = (DataTable)dgvStudents.DataSource;
                        //    dgvStudents.DataSource = null;
                        //    dtStudents.Rows.Remove(dr);
                        //    dgvStudents.DataSource = dtStudents;
                        //}
                        //else
                        //{
                        //    MessageBox.Show("学生信息删除失败", "删除提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}

                    }

                }
            }
                

        }
        /// <summary>
        /// 多行删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //获取到要删除的数据的StudentId
            //判断选择的个数如果为0，没有选择，提示用户选择，大于0，继续
            //删除操作 事务，sql事务，代码里面启动事务
            List<int> listIds = new List<int>();
            for (int i = 0; i < dgvStudents.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cell = dgvStudents.Rows[i].Cells["colCheck"] as DataGridViewCheckBoxCell;
                bool chk = Convert.ToBoolean(cell.Value);
                if (chk)
                {
                    //获取行数据
                    DataRow dr = (dgvStudents.Rows[i].DataBoundItem as DataRowView).Row;
                    int stuId =(int) dr["StudentId"];
                    listIds.Add(stuId);
                }
            }
            //真删除
            if (listIds.Count == 0)
            {
                MessageBox.Show("请选择要删除的学生信息", "删除学生提示");
                return;
            }
            else
            {
                if (MessageBox.Show("确定要删除吗？", "删除信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int count = 0;
                    //启动事务
                    using (SqlConnection conn=new SqlConnection(SqlHelper.connString))
                    {
                        //事务启动是通过conn来开启，并且conn是open的状态
                        conn.Open();  
                        SqlTransaction trans = conn.BeginTransaction();
                        //sqlcommand 事务执行
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.Transaction = trans; 
                        try
                        {
                            foreach (int id in listIds)
                            {
                                cmd.CommandText = "delete from StudentInfo where StudentId=@StudentId";
                                SqlParameter para =new SqlParameter("@StudentId", id);
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add(para);
                                count += cmd.ExecuteNonQuery();
                            }
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            MessageBox.Show("删除学生出现异常", "删除学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    if (count==listIds.Count)
                    {
                        MessageBox.Show("删除学生成功", "删除学生提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //手动刷新
                        DataTable dtStudents = (DataTable)dgvStudents.DataSource;
                        string idStr = string.Join(",",listIds);
                        DataRow[] rows = dtStudents.Select("StudentId in ("+idStr+")");
                        foreach (DataRow dr in rows)
                        {
                            dtStudents.Rows.Remove(dr);
                        }
                        dgvStudents.DataSource = dtStudents;
                    }

                }
            }

            


        }
    }
}
