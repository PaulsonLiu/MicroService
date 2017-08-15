using System;
using System.Web;

namespace iFramework.Util
{
    /// <summary>
    /// 页面名  ：Cookie读写类<br/>
    /// 说明    ：读取,设置，清除Cookie<br/>
    /// 作者    ：易小辉<br/>
    /// 创建时间：2010-4-17<br/>
    /// 最后修改：2011-3-7<br/>
    /// </summary>
    public class CookieHelper
    {
        #region 写Cookie
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="strValue">值</param>
        public static void SetCookie(string cookieName, string strValue)
        {
            SetCookie(cookieName, string.Empty, strValue, 0, string.Empty, string.Empty);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void SetCookie(string cookieName, string strValue, int expires)
        {
            SetCookie(cookieName, string.Empty, strValue, expires, string.Empty, string.Empty);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        /// <param name="path">路径</param>
        /// <param name="domain">域</param>
        public static void SetCookie(string cookieName, string strValue, int expires, string path, string domain)
        {
            SetCookie(cookieName, string.Empty, strValue, expires, path, domain);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="key">键</param>
        /// <param name="strValue">值</param>
        public static void SetCookie(string cookieName, string key, string strValue)
        {
            SetCookie(cookieName, key, strValue, 0, string.Empty, string.Empty);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="key">键</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void SetCookie(string cookieName, string key, string strValue, int expires)
        {
            SetCookie(cookieName, key, strValue, expires, string.Empty, string.Empty);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="key">键</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        /// <param name="path">路径</param>
        /// <param name="domain">域</param>
        public static void SetCookie(string cookieName, string key, string strValue, int expires, string path, string domain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName);
            }
            if (!string.IsNullOrEmpty(key))
            {
                cookie[key] = Publics.UrlEncode(strValue);
            }
            else
            {
                cookie.Value = Publics.UrlEncode(strValue);
            }
            if (!string.IsNullOrEmpty(path)) { cookie.Path = path; }
            if (!string.IsNullOrEmpty(domain)) { cookie.Domain = domain; }
            if (expires != 0) { cookie.Expires = DateTime.Now.AddMinutes(expires); }
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        #endregion

        #region 读Cookie

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                return Publics.UrlDecode(HttpContext.Current.Request.Cookies[cookieName].Value.ToString());
            }
            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="key">键</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string cookieName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null && HttpContext.Current.Request.Cookies[cookieName][key] != null)
            {

                return Publics.UrlDecode(HttpContext.Current.Request.Cookies[cookieName][key].ToString());
            }

            return "";
        }

        #endregion

        #region 清除Cookies

        /// <summary>
        /// 清除指定名称的cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        public static void ClearCookie(string cookieName)
        {
            ClearCookie(cookieName, string.Empty, "");
        }

        /// <summary>
        /// 清除指定名称的cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        public static void ClearCookie(string cookieName, string path)
        {
            ClearCookie(cookieName, string.Empty, string.Empty);
        }

        /// <summary>
        /// 清除指定名称的cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="domain">域</param>
        public static void ClearCookie(string cookieName, string path, string domain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Values.Clear();
                if (!string.IsNullOrEmpty(domain))
                {
                    cookie.Domain = domain;
                }
                if (!string.IsNullOrEmpty(path))
                {
                    cookie.Path = path;
                }
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.SetCookie(cookie);
            }
        }
        #endregion

    }
}
