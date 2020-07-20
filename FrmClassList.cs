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

        private void dgvClassList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewCell cell = dgvClassList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                DataRow dr = (dgvClassList.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                int classId = (int)dr["ClassId"];
                if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString()=="修改")
                {

                }
                else if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString() == "删除")
                {
                    //单条删除,先删除班级下的学生，再删除班级
                    string delStudent = "delete from StudentInfo where ClassId=@ClassId";
                    //班级信息的sql
                    string delClass = "delete from ClassInfo where ClassId=@ClassId";
                    //参数
                    SqlParameter[] para = { new SqlParameter("@ClassId", classId) };
                    List<CommandInfo> listComs = new List<CommandInfo>();
                    CommandInfo comStudent = new CommandInfo()
                    {
                        CommandText = delStudent,
                        IsProc = false,
                        parameters=para
                        
                    };
                    listComs.Add(comStudent);

                    CommandInfo comClass = new CommandInfo()
                    {
                        CommandText = delClass,
                        IsProc=false,
                        parameters=para
                    };
                    listComs.Add(comClass);
                    //执行
                    bool b1 = SqlHelper.ExecuteTrans(listComs);
                    if (b1)
                    {
                        MessageBox.Show("班级信息删除成功", "删除学生提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //手动刷新
                        DataTable dtClass =(DataTable) dgvClassList.DataSource;
                        dtClass.Rows.Remove(dr);
                        dgvClassList.DataSource = dtClass;
                    }
                    else
                    {
                        MessageBox.Show("班级信息删除失败", "删除学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //获取到要删除的数据的ClassId
            //判断选择的个数如果为0，没有选择，提示用户选择，大于0，继续
            //删除操作 事务，sql事务，代码里面启动事务
            List<int> listIds = new List<int>();
            for (int i = 0; i < dgvClassList.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cell = dgvClassList.Rows[i].Cells["colCheck"] as DataGridViewCheckBoxCell;
                bool chk = Convert.ToBoolean(cell.Value);
                if (chk)
                {
                    //获取行数据
                    DataRow dr = (dgvClassList.Rows[i].DataBoundItem as DataRowView).Row;
                    int stuId = (int)dr["ClassId"];
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
                if (MessageBox.Show("确定要删除这些班级以及相关的学生吗？", "删除信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string delStudent = "delete from StudentInfo where ClassId=@ClassId";
                    string delClass = "delete from ClassInfo where ClassId=@ClassId";
                    List<CommandInfo> listComs = new List<CommandInfo>();
                    foreach (int  id in listIds)
                    {
                        //参数
                        SqlParameter[] para =
                        {
                            new SqlParameter("@ClassId",id)
                        };

                        CommandInfo comStudent = new CommandInfo()
                        {
                            CommandText = delStudent,
                            IsProc = false,
                            parameters = para
                        };
                        listComs.Add(comStudent);

                        CommandInfo comClass = new CommandInfo()
                        {
                            CommandText = delClass,
                            IsProc = false,
                            parameters = para
                        };
                        listComs.Add(comClass);

                    }
                    bool b1 = SqlHelper.ExecuteTrans(listComs);
                    if (b1)
                    {
                        MessageBox.Show("删除学生成功", "删除学生提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // 手动刷新
                        DataTable dtClass = (DataTable)dgvClassList.DataSource;
                        string idStr = string.Join(",", listIds);
                        DataRow[] rows = dtClass.Select("ClassId in (" + idStr + ")");
                        foreach (DataRow dr in rows)
                        {
                            dtClass.Rows.Remove(dr);
                        }
                        dgvClassList.DataSource = dtClass;
                    }
                 }




        }
    }
}
    }

