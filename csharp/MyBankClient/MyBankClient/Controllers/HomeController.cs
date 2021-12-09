using System;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace MyBankClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeeLogin()
        {
            return View();
        }

        //*************** 方法 ***************//

        //登录方法
        //前端页面提交的ajax请求，通过account获得depositor , 返回JsonResult
        public JsonResult UserLogin(string account, string pwd)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync("https://localhost:44370/api/depositor/"+ account).Result;
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    throw e;
                }

                //获得接口的返回值
                JObject json= JObject.Parse(response.Content.ReadAsStringAsync().Result);
                string result_dId = json["dId"].ToString();
                string result_pwd = json["pwd"].ToString(); 
                string result_dName = json["dName"].ToString();
                string result_deposit = json["deposit"].ToString();
                string myflag = "";

                if (result_pwd.Equals(pwd))
                {
                    myflag = "success";
                }
                else
                {
                    myflag = "error";
                }

                //将一些data储存到session中。
                Session["dId"] = result_dId;
                Session["dName"] = result_dName;
                Session["deposit"] = result_deposit;

                //最终返回结果
                var person = new
                {
                    dId = result_dId,
                    pwd = result_pwd,
                    dName = result_dName,
                    deposit = result_deposit,
                    Myflag = myflag
                };
                JsonResult jsonResult = Json(person, JsonRequestBehavior.AllowGet);



                return jsonResult;
            }
        }


        //登录方法
        //前端页面提交的ajax请求，通过account获得depositor , 返回JsonResult
        public JsonResult AJAXEmployeeLogin(string account, string pwd)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync("https://localhost:44370/api/employee/" + account).Result;
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    throw e;
                }

                //获得接口的返回值
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                string result_eId = json["eId"].ToString();
                string result_pwd = json["pwd"].ToString();
                string result_ename = json["ename"].ToString();
                string myflag = "";

                if (result_pwd.Equals(pwd))
                {
                    myflag = "success";
                }
                else
                {
                    myflag = "error";
                }

                //将一些data储存到session中。
                Session["eId"] = result_eId;
                Session["ename"] = result_ename;

                //最终返回结果
                var person = new
                {
                    eId = result_eId,
                    ename = result_ename,
                    Myflag = myflag
                };
                JsonResult jsonResult = Json(person, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
        }

    }
}