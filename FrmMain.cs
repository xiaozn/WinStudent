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


    }
}
