using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XDPTPM
{
    public class Manage_cookies
    {
        public void AddCookieOrder(string name, List<order_dish> list,int time)
        {
            HttpCookie jsonCookie = new HttpCookie(name);
            jsonCookie.Expires = DateTime.Now.AddMinutes(time);
            string json = JsonConvert.SerializeObject(list);
            jsonCookie.Value = json;
            HttpContext.Current.Response.Cookies.Add(jsonCookie);
        }
        public void AddCookieCheckout(string name, List<checkOut> list,int time)
        {
            HttpCookie jsonCookie = new HttpCookie(name);
            jsonCookie.Expires = DateTime.Now.AddMinutes(time);
            string json = JsonConvert.SerializeObject(list);
            jsonCookie.Value = json;
            HttpContext.Current.Response.Cookies.Add(jsonCookie);
        }

        public void RemoveAllCookie()
        {
            var cookieNames = new List<string>();
            foreach (string cookieName in HttpContext.Current.Request.Cookies)
            {
                cookieNames.Add(cookieName);
            }

            foreach (string cookieName in cookieNames)
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public List<order_dish> ReturnOrderList()
        {
            List<order_dish> order_s = new List<order_dish>();
            string json = HttpContext.Current.Request.Cookies["OrderList"].Value;
            order_s = JsonConvert.DeserializeObject<List<order_dish>>(json);
            return order_s;
        }

        public List<checkOut> ReturnListCheckOut()
        {
            List<checkOut> list = new List<checkOut>();

            HttpCookie listCookie = HttpContext.Current.Request.Cookies["checkOut"];
            string stringListCheckout = listCookie.Value;
            list = JsonConvert.DeserializeObject<List<checkOut>>(stringListCheckout);
            return list;
        }
    }
}