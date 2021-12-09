//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Security;
//using Newtonsoft.Json;
//using WebArticle.Models;

//namespace WebArticle.Utilities
//{
//    public class TicketTool
//    {
//        public static void SetCookie(Customer userInfo, DateTime? issueDateTime = null, DateTime? experation = null, bool isPersistent = true)
//        {
//            if (issueDateTime == null)
//            {
//                issueDateTime = DateTime.Now;
//            }
//            if (experation == null)
//            {
//                //设置COOKIE默认为16小时
//                experation = DateTime.Now.AddHours(16);
//            }
//            string userData = JsonConvert.SerializeObject(userInfo);
//            //生成验证票据，其中包括用户名、生效时间、过期时间、是否永久保存和用户数据等。
//            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userInfo.Name, (DateTime)issueDateTime, (DateTime)experation, isPersistent, userData, FormsAuthentication.FormsCookiePath);
//            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
//            cookie.Expires = (DateTime)experation;
//            HttpResponse response = HttpContext.Current.Response;
//            //指定客户端脚本是否可以访问[默认为false]
//            cookie.HttpOnly = true;
//            //指定统一的Path，比便能通存通取
//            cookie.Path = "/";
//            //设置跨域,这样在其它二级域名下就都可以访问到了 同一个网站下 
//            response.AppendCookie(cookie);
//        }

//        /// <summary>
//        /// 获取登录的用户信息
//        /// </summary>
//        /// <returns></returns>
//        /// <returns></returns>
//        public static Customer GetUserInfo()
//        {
//            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
//            if (authCookie != null)
//            {
//                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
//                if (authTicket != null)
//                {
//                    string userData = authTicket.UserData;
//                    var userInfo = JsonConvert.DeserializeObject<Customer>(userData);
//                    return userInfo;
//                }
//            }
//            return null;
//        }

//        /// <summary>
//        /// 通过此法判断登录
//        /// </summary>
//        /// <returns>已登录返回true</returns>
//        public static bool IsLogin()
//        {
//            return HttpContext.Current.User.Identity.IsAuthenticated;
//        }

//        /// <summary>
//        /// 退出登录
//        /// </summary>
//        public static void Logout()
//        {
//            FormsAuthentication.SignOut();
//        }

//        /// <summary>
//        /// 取得登录用户名
//        /// </summary>
//        /// <returns></returns>
//        public static string GetUserName()
//        {
//            return HttpContext.Current.User.Identity.Name;
//        }

//        /// <summary>
//        /// 取得票据中数据
//        /// </summary>
//        /// <returns></returns>
//        public static string GetUserData()
//        {
//            var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;
//            if (formsIdentity != null)
//            {
//                return formsIdentity.Ticket.UserData;
//            }
//            return string.Empty;
//        }

     
//    }
//}