using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudent
{
    public class CommandInfo
    {
        public string CommandText;//sql或者存储过程名
        public DbParameter[] parameters;//参数列表
        public bool IsProc;//是否是存储过程
        public CommandInfo()
        {

        }
        public CommandInfo(string comText,bool isProc)
        {
            this.CommandText = comText;
            this.IsProc = isProc;
        }
        public CommandInfo(string sqlText,bool isProc,DbParameter[] para)
        {
            this.CommandText = sqlText;
            this.IsProc = isProc;
            this.parameters = para;
        }

    }
}
