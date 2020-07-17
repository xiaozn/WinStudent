using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WinStudent
{
    public class SqlHelper
    {
        //获取服务器地址和登陆账号密码
        public static readonly string connString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        public static object ExecuteScalar(string sql,params SqlParameter[] paras)
        {
            //string connString = "server=.;database=StudentDB;Intergrated Security=true;";//windows身份验证
            object o = null;  
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //创建command对象
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.CommandType = CommandType.StoredProcedure;//如果执行存储过程,需要加上这句
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(paras);
                //打开连接
                conn.Open();//最晚打开,最早关闭
                //执行命令,要求必须在连接状态下
                o = cmd.ExecuteScalar();//执行查询,返回结果集第一行第一列的值,忽略其他行或者列
            } 
            return o;
        }

        public static DataTable GetDataTable(string sql,params SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            using(SqlConnection conn=new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (paras != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(paras);
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = cmd;
                sqlDataAdapter.Fill(dt);
            }

            return dt;
        }

        public static int ExecuteNoQuery(string sql,params SqlParameter[] paras)
        {
            using(SqlConnection conn=new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (paras != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(paras);
                }
            }

        }
    }
}
