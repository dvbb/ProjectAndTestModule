using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebArticle.Utilities;

namespace MyBankClient.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveMoney()
        {
            return View();
        }
        public ActionResult SaveMoneyForm()
        {
            return View();
        }
        public ActionResult Withdraw()
        {
            return View();
        }
        public ActionResult WithdrawForm()
        {
            return View();
        }
        public ActionResult OpenAccount()
        {
            return View();
        }
        public ActionResult UpdatePwdFrm()
        {
            return View();
        }





        //获取dId的存款记录
        //前端页面提交的ajax请求，返回JsonResult
        public string GetAmountBydId(string dId)
        {
            using (HttpClient client = new HttpClient())
            {
                Task<string> responseMessage = client.GetStringAsync("https://localhost:44370/api/amount/" + dId);
                var response = responseMessage.Result;
                Console.WriteLine(response);

                string str = HttpHelper.HttpGet($"https://localhost:44370/api/amount/" + dId);
                return str;

            }
        }


        //开户
        public string OpenAmount(string dId, string dName, string pwd)
        {

            #region
            //using (HttpClient client = new HttpClient())
            //{

            //    Task<string> responseMessage = client.GetStringAsync("https://localhost:44370/api/depositor/");
            //    var response = responseMessage.Result;
            //    Console.WriteLine(response);

            //    Dictionary<string, string> pairs = new Dictionary<string, string> {
            //        { "dId",dId},
            //        { "dName",dName},
            //        { "pwd",pwd}
            //    };

            //    string str = HttpHelper.HttpPost($"https://localhost:44370/api/depositor/", pairs);
            //    return str;

            //}

            //Dictionary<string, string> pairs = new Dictionary<string, string> {
            //        { "dId",dId},
            //        { "dName",dName},
            //        { "pwd",pwd}
            //    };

            //string result = "";
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://localhost:44370/api/depositor/");
            //req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded";

            //#region 添加Post 参数
            //StringBuilder builder = new StringBuilder();
            //int i = 0;
            //foreach (var item in pairs)
            //{
            //    if (i > 0)
            //        builder.Append("&");
            //    builder.AppendFormat("{0}={1}", item.Key, item.Value);
            //    i++;
            //}
            //byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            //req.ContentLength = data.Length;
            //using (Stream reqStream = req.GetRequestStream())
            //{
            //    reqStream.Write(data, 0, data.Length);
            //    reqStream.Close();
            //}
            //#endregion

            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            //Stream stream = resp.GetResponseStream();
            ////获取响应内容
            //using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            //{
            //    result = reader.ReadToEnd();
            //}
            //return result;
            //        { "dId",dId},
            //        { "dName",dName},
            //        { "pwd",pwd}
            #endregion

            Dictionary<string, string> pairs = new Dictionary<string, string>();
            pairs.Add("id", dId);
            pairs.Add("name", dName);
            pairs.Add("pwd", pwd);
            string str = JsonConvert.SerializeObject(pairs);
            var content = new StringContent(str);
            using (HttpClient client = new HttpClient())
            {
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                string uri = "https://localhost:44370/api/depositor/" + dId + "/" + pwd + "/" + dName;
                var response = client.PostAsync(uri, content).Result;
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    throw e;
                }
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        //存款
        //"https://localhost:44370/api/amount" -H "accept: text/plain" -H "Content-Type: application/json-patch+json" -d "{\"pId\":\"3fa85f64-5717-4562-b3fc-2c963f6222a6\",\"dId\":\"18877140000\",\"dName\":\"aya\",\"addr\":\"上海浦东\",\"dType\":\"活期\",\"dTime\":\"2021-01-06T06:06:01\",\"rate\":0.3,\"deposit\":3000,\"dStatus\":false}"
        public string PostSaveMoney(string dId, string dName, string addr, string dType, string rate, string deposit)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            pairs.Add("did", dId);
            pairs.Add("dName", dName);
            pairs.Add("addr", addr);
            pairs.Add("dType", dType);
            pairs.Add("rate", rate);
            pairs.Add("deposit", deposit);

            //存入session-为打印存款单提供数据
            Session["save_pid"] = Guid.NewGuid();
            Session["save_did"] = dId;
            Session["save_dName"] = dName;
            Session["save_addr"] = addr;
            Session["save_dType"] = dType;
            Session["save_deposit"] = deposit;
            Session["save_startTime"] = DateTime.Now.ToString("yyyy/MM/dd");
            Session["save_rate"] = rate;



            string str = JsonConvert.SerializeObject(pairs);
            var content = new StringContent(str);
            using (HttpClient client = new HttpClient())
            {
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                var response = client.PostAsync("https://localhost:44370/api/amount", content).Result;
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    throw e;
                }
                return response.Content.ReadAsStringAsync().Result;
            }
        }



        //取款
        public string PutWithdraw(string Id)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            pairs.Add("id", Id);
            string str = JsonConvert.SerializeObject(pairs);
            var content = new StringContent(str);
            using (HttpClient client = new HttpClient())
            {

                #region 先GET方法获取id的具体项目，存入session中
                var GetResponse = client.GetAsync("https://localhost:44370/api/amount/pid/" + Id).Result;

                //获得接口的返回值
                JObject json = JObject.Parse(GetResponse.Content.ReadAsStringAsync().Result);
                string result_pId = json["pId"].ToString();
                string result_dId = json["dId"].ToString();
                string result_dType = json["dType"].ToString();
                DateTime result_dTime = (DateTime)json["dTime"];
                decimal result_deposit = (decimal)json["deposit"];
                decimal result_rate = (decimal)json["rate"];

                //将一些data储存到session中。
                Session["pId"] = result_pId;
                Session["dId"] = result_dId;
                Session["dType"] = result_dType;
                Session["startTime"] = result_dTime.ToString("yyyy/MM/dd");
                Session["endTime"] = DateTime.Now.ToString("yyyy/MM/dd");

                Session["deposit"] = result_deposit;
                Session["rate"] = result_rate;

                int subdays = DateTime.Now.Subtract(result_dTime).Days;
                decimal interest = result_deposit  / 365 * subdays * result_rate;
                Session["interest"] = interest;
                Session["total"] = interest + result_deposit;

                #endregion


                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                string uri = "https://localhost:44370/api/amount/" + Id;
                var response = client.PutAsync(uri, content).Result;
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    throw e;
                }




                return response.Content.ReadAsStringAsync().Result;
            }
        }


        //更新密码
        public JsonResult UpdatePwd(string id,string oldpwd,string newpwd)
        {
            using (HttpClient client = new HttpClient())
            {
                string uri = "https://localhost:44370/api/employee/" + id;
                var response = client.GetAsync(uri).Result;
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    throw e;
                }


                var isupdate = false;

                //获得接口的返回值
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                string result_eId = json["eId"].ToString();
                string result_pwd = json["pwd"].ToString();

                //密码相同，执行更新
                if (result_pwd == oldpwd) {

                    isupdate = true;

                    Dictionary<string, string> pairs = new Dictionary<string, string>();
                    pairs.Add("id", id);
                    pairs.Add("pwd", newpwd);
                    string str2 = JsonConvert.SerializeObject(pairs);
                    var content2 = new StringContent(str2);
                    using (HttpClient client2 = new HttpClient())
                    {
                        content2.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                        string uri2 = "https://localhost:44370/api/employee/"+id+"/"+newpwd ;
                        var response2 = client.PutAsync(uri2, content2).Result;
                        try
                        {
                            response.EnsureSuccessStatusCode();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }






                //最终返回结果
                var Myflag = new
                {
                    Myflag = isupdate
                };
                JsonResult jsonResult = Json(Myflag, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
        }




    }
}