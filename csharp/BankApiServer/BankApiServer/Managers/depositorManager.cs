using BankApiServer.Models;
using BankApiServer.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BankApiServer.Managers
{
    public static class depositorManager
    {
        public static Depositor GetDepositor(string id)
        {
            Depositor depositor = new Depositor();
            SqlParameter[] paras =
            {
                new SqlParameter("@did",id)
            };
            SqlDataReader dr = sqlHelper.SearchSQL("proc_depositor_dId_select", paras);
            while (dr.Read())
            {
                depositor.dId = dr["dId"].ToString();
                depositor.pwd = dr["pwd"].ToString();
                depositor.dName = dr["dName"].ToString();
                depositor.deposit = (decimal)dr["deposit"];
            }
            return depositor;
        }
    }
}