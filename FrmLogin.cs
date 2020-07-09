using System;
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
                //建立与数据库的连接
                //连接数据库的字符串
                //string connString = "server=.;database=StudentDB;Intergrated Security=true;";//windows身份验证
                string connString = "server=.;database=StudentDB;uid=sa;pwd=sa123;";//SQL验证方式
                SqlConnection conn = new SqlConnection(connString);
                //写查询语句
                string sql = "select count(1) from UserInfos where UserName='" + uName + "' and UserPwd='" + uPwd + "'";
                //创建command对象
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.CommandType = CommandType.StoredProcedure;//如果执行存储过程,需要加上这句
                //打开连接
                conn.Open();//最晚打开,最早关闭
                //执行命令,要求必须在连接状态下
                object o = cmd.ExecuteScalar();//执行查询,返回结果集第一行第一列的值,忽略其他行或者列
                //关闭连接
                conn.Close();
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
