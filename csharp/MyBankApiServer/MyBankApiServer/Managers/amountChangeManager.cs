using BankApiServer.utilities;
using MyBankApiServer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyBankApiServer.Managers
{
    public static class amountChangeManager
    {
        /// <summary>
        /// 查询某个用户的储蓄记录
        /// </summary>
        /// <param name="did"></param>
        /// <returns></returns>
        public static List<AmountChange> GetAmountChange(string did)
        {
            List<AmountChange> list = new List<AmountChange>();
            SqlParameter[] paras = {
                new SqlParameter("@did",did)
            };
            SqlDataReader dr = SqlHelper.SearchSQL("proc_amountChange_did_select", paras);
            while (dr.Read())
            {
                AmountChange amountChange = new AmountChange();
                amountChange.pId = (Guid)dr["pId"];
                amountChange.dId = dr["dId"].ToString();
                amountChange.dName = dr["dName"].ToString();
                amountChange.addr = dr["addr"].ToString();
                amountChange.dType = dr["dType"].ToString();
                amountChange.dTime = (DateTime)dr["dTime"];
                amountChange.rate = (double)dr["rate"];
                amountChange.deposit = (decimal)dr["deposit"];
                amountChange.dStatus = (bool)dr["dStatus"];
                list.Add(amountChange);
            }
            return list;
        }

        /// <summary>
        /// 插入一个储蓄记录
        /// </summary>
        /// <param name="did"></param>
        /// <param name="dname"></param>
        /// <param name="addr"></param>
        /// <param name="dtype"></param>
        /// <param name="rate"></param>
        /// <param name="deposit"></param>
        /// <param name="dstatus"></param>
        /// <returns>影响的行数</returns>
        public static int InsertAmountChange(string did, string dname, string addr, string dtype, double rate, decimal deposit)
        {
            AmountChange amountChange = new AmountChange();
            SqlParameter[] paras =
            {
                new SqlParameter("@pid",Guid.NewGuid().ToString()),
                new SqlParameter("@did",did),
                new SqlParameter("@dname",dname),
                new SqlParameter("@addr",addr),
                new SqlParameter("@dtype",dtype),
                new SqlParameter("@dtime",DateTime.Now.ToString()),
                new SqlParameter("@rate",rate),
                new SqlParameter("@deposit",deposit),
                new SqlParameter("@dstatus",false)
            };
            int result = SqlHelper.UpdateSQL("proc_amountChange_all_insert", paras);
            return result;
        }

        /// <summary>
        /// 更新一条储蓄记录的状态（取钱后，status设为true）
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static int UpdateAmountChange(string pid)
        {
            AmountChange amountChange = new AmountChange();
            SqlParameter[] paras =
            {
                new SqlParameter("@pid",pid)
            };
            int result = SqlHelper.UpdateSQL("proc_amountChange_pid_delete", paras);
            return result;
        }

        /// <summary>
        /// 通过pid获取某一条记录
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static AmountChange GetAmountChangeByPid(string pid)
        {
            AmountChange amountChange = new AmountChange();
            SqlParameter[] paras = {
                new SqlParameter("@pid",pid)
            };
            SqlDataReader dr = SqlHelper.SearchSQL("proc_amountChange_pid_select", paras);
            while (dr.Read())
            {
                amountChange.pId = (Guid)dr["pId"];
                amountChange.dId = dr["dId"].ToString();
                amountChange.dName = dr["dName"].ToString();
                amountChange.addr = dr["addr"].ToString();
                amountChange.dType = dr["dType"].ToString();
                amountChange.dTime = (DateTime)dr["dTime"];
                amountChange.rate = (double)dr["rate"];
                amountChange.deposit = (decimal)dr["deposit"];
                amountChange.dStatus = (bool)dr["dStatus"];
            }
            return amountChange;
        }
    }
}
