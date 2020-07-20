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
        /// <summary>
        /// 返回受影响的行数,增删改都可以用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExecuteNoQuery(string sql,params SqlParameter[] paras)
        {
            int count = 0;
            using(SqlConnection conn=new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (paras != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(paras);
                }
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            return count;

        }

        public static SqlDataReader ExecuteReader(string sql,params SqlParameter[] paras)
        {
            SqlConnection conn = new SqlConnection(connString);
            
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (paras.Length > 0)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(paras);
                }
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw new Exception("执行查询出现异常", ex);
                
            }
            
        }

        public static bool ExecuteTrans(List<CommandInfo> comList)
        {
            using (SqlConnection conn=new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = trans;
                try
                {
                    int count = 0;
                    for (int i = 0; i < comList.Count; i++)
                    {
                        cmd.CommandText = comList[i].CommandText;
                        if (comList[i].IsProc)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                        }
                        else
                        {
                            cmd.CommandType = CommandType.Text;
                        }
                        if (comList[i].parameters.Length>0)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(comList[i].parameters);
                        }
                        count += cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("执行事务出现异常",ex);
                }
            }
        }
    }
}
