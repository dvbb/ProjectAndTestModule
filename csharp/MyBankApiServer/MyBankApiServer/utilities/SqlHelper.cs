using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace BankApiServer.utilities
{
    public static class SqlHelper
    {
        static string connStr = "server=81.68.214.241 ;database=MyBank;uid=sa;pwd=zY2zdphpcXmyr7m3";
        static SqlConnection conn;


        /// <summary>
        /// 查询数据库，带参
        /// </summary>
        /// <param name="spName">存储过程名am>
        /// <param name="paras">键值对参数</param>
        /// <returns> 返回SqlDataReader对象 </returns>
        public static SqlDataReader SearchSQL(string spName, SqlParameter[] paras)
        {
            conn = new SqlConnection(connStr);
            try
            {
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(paras);
                conn.Open();
                SqlDataReader dataread = cmd.ExecuteReader();
                return dataread;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\nerror!!\n\n");
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="paras"></param>
        /// <returns> 返回int值，改变的行数 </returns>
        public static int UpdateSQL(string spName, SqlParameter[] paras)
        {

            using(conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(spName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(paras);
                    conn.Open();
                    int dataread = cmd.ExecuteNonQuery();
                    return dataread;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\nerror!!\n\n");
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

    }
}