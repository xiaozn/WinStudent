﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinStudent
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //获取用户输入信息
            string uName = txtUserName.Text.Trim();
            string uPwd = txtUserPwd.Text.Trim();
            //判读是否为空
            if (string.IsNullOrEmpty(uName))
            {
                MessageBox.Show("请输入用户名", "登陆提示",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(uPwd))
            {
                MessageBox.Show("请输入密码", "密码提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtUserPwd.Focus();
                return;
            }

            //与数据库通讯,账号密码比对
            {
                
                //写查询语句
                string sql = "select count(1) from UserInfo where UserName=@UserName and UserPwd=@UserPwd";
                
                SqlParameter[] paras =
                {
                    new SqlParameter("@UserName", uName),
                    new SqlParameter("@UserPwd", uPwd)
                };
                object o = SqlHelper.ExecuteScalar(sql, paras);
                //处理结果
                //根据返回的结果,进行不同的提示和页面跳转
                if (o==null||o==DBNull.Value || ((int)o) == 0)
                {
                    MessageBox.Show("登陆账号或者密码有错,请检查!", "登陆提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    int count = (int)o;
                    MessageBox.Show("登陆成功", "登陆提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //转到主页面
                    FrmMain frmMain = new FrmMain();
                    frmMain.Show();
                    this.Hide();
                }
            }

            


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();//主页面启动窗体退出
            //Application.Exit();//其他页面退出
        }
    }
}
