using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinStudent
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 新增学生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subAddStudent_Click(object sender, EventArgs e)
        {
            FrmAddStudent frmAddStudent = new FrmAddStudent();
            frmAddStudent.MdiParent = this;
            frmAddStudent.Show();
        }
        /// <summary>
        /// 学生列表,不可以打开多个列表--单例或者判断窗体是否打开 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subStudentList_Click(object sender, EventArgs e)
        {
            bool isOpen = CheckFormIsOpen(typeof(FrmAddStudent).Name);
            if (!isOpen)
            {
                FrmStudentList frmStudentList = FrmStudentList.CreateInstance();
                frmStudentList.MdiParent = this;
                frmStudentList.Show();
            }
        }
        /// <summary>
        /// 检查窗体是否已经打开
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        private bool CheckFormIsOpen(string formName)
        {
            bool isOpen = false;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == formName)
                {
                    isOpen = true;
                    item.Activate();
                    break;
                }
            }
            return isOpen;
        }
        /// <summary>
        /// 新增班级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subAddClass_Click(object sender, EventArgs e)
        {
            FrmAddClass frmAddClass = new FrmAddClass();
            frmAddClass.MdiParent = this;
            frmAddClass.Show();
        }
        /// <summary>
        /// 班级列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subClassList_Click(object sender, EventArgs e)
        {
            bool b1 = CheckFormIsOpen(typeof(FrmClassList).Name);
            if (!b1)
            {
                FrmClassList frmClassList = new FrmClassList();
                frmClassList.MdiParent = this;
                frmClassList.Show();
            }
        }
        /// <summary>
        /// 年级列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subGradeList_Click(object sender, EventArgs e)
        {
            bool b1 = CheckFormIsOpen(typeof(FrmGradeList).Name);
            if (!b1)
            {
                FrmGradeList frmGradeList = new FrmGradeList();
                frmGradeList.MdiParent = this;
                frmGradeList.Show();
            }
        }
        /// <summary>
        /// 窗体关闭，退出应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要退出系统吗？", "退出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
            
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitSystem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
