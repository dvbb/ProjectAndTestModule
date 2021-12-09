using BankApiServer.Models;
using BankApiServer.utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BankApiServer.Managers
{
    public static class employeeManager
    {

        public static List<Employee> GetEmployee()
        {
            List<Employee> list = new List<Employee>();
            SqlParameter[] paras = { };
            SqlDataReader dr = SqlHelper.SearchSQL("proc_employee_none_select", paras);
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.eId = dr["eId"].ToString();
                employee.pwd = dr["pwd"].ToString();
                employee.ename = dr["eName"].ToString();
                list.Add(employee);
            }
            return list;
        }

        public static Employee GetEmployee(string id)
        {
            Employee employee = new Employee();
            SqlParameter[] paras =
            {
                new SqlParameter("@eid ",id)
            };
            SqlDataReader dr = SqlHelper.SearchSQL("proc_employee_eId_select", paras);
            while (dr.Read())
            {
                employee.eId = dr["eId"].ToString();
                employee.pwd = dr["pwd"].ToString();
                employee.ename = dr["eName"].ToString();
            }
            return employee;
        }


        public static int UpdatePwd(string eid, string pwd)
        {
            SqlParameter[] paras =
            {
                new SqlParameter("@eid ",eid),
                new SqlParameter("@pwd",pwd)
            };
            int result = SqlHelper.UpdateSQL("proc_employee_pwd_update", paras);
            return result;
        }
    }
}