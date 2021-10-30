using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CCDto.common.NetCore;
using Microsoft.AspNetCore.Http;
using HttpContext = CCDto.common.NetCore.HttpContext;

namespace CCDto.common
{
    public class DNTRequest
    {

        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public bool IsPost()
        {
            return HttpContext.Current.Request.Method.Equals("POST");
        }
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.Method.Equals("GET");
        }

        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        //public static string GetServerString(string strName)
        //{
        //    //
        //    if (HttpContext.Current.Request.ServerVariables[strName] == null)
        //    {
        //        return "";
        //    }
        //    return HttpContext.Current.Request.ServerVariables[strName].ToString();
        //}

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.Headers["Referer"].ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;

        }

        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = HttpContext.Current.Request;
            //if (!request..IsDefaultPort)
            //{
            //    return Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            //}
            return HttpContext.Current.Request.Host.Value;
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Host.Value;
            //return HttpContext.Current.Request.Url.Host;
        }


        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        //public static string GetRawUrl()
        //{
        //    return HttpContext.Current.Request.GetEncodedUrl();
        //}

        public static string GetUrl()
        {
            return GetRouteUrl();
        }

        public static string GetRouteUrl()
        {
            return HttpContext.Current.Request.Path + HttpContext.Current.Request.QueryString;
        }

        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Headers["User-Agent"].ToString().ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.Headers["Referer"].ToString() == null)
            {
                return false;
            }
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
            string tmpReferrer = HttpContext.Current.Request.Headers["Referer"].ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        //public static string GetUrl()
        //{
        //    try
        //    {
        //        return HttpContext.Current.Request.Url.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }
        //return new StringBuilder()
        //        .Append(request.Scheme)
        //        .Append("://")
        //        .Append(request.Host)
        //        .Append(request.PathBase)
        //        .Append(request.Path)
        //        .Append(request.QueryString)
        //        .ToString();
        //}


        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            try
            {
                var o = HttpContext.Current.Items[strName];
                if (string.IsNullOrWhiteSpace(HttpContext.Current.Request.Query[strName]))
                {
                    return "";
                }
                return Utils.ChkSQL(HttpContext.Current.Request.Query[strName]);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        //public static string GetPageName()
        //{
        //    string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
        //    return urlArr[urlArr.Length - 1].ToLower();
        //}

        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form != null ? HttpContext.Current.Request.Form.Count : 0 + (HttpContext.Current.Request.Query != null ? HttpContext.Current.Request.Query.Count : 0);
        }


        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            try
            {
                var o = HttpContext.Current.Items[strName];
                if (string.IsNullOrWhiteSpace(HttpContext.Current.Request.Form[strName]))
                {
                    return "";
                }
                return Utils.ChkSQL(HttpContext.Current.Request.Form[strName]);
            }
            catch
            {
                return "";
            }

            //if (HttpContext.Current.Request.Form[strName] == null)
            //{
            //    return "";
            //}
            //return Utils.ChkSQL(HttpContext.Current.Request.Form[strName]);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName, string empty = "")
        {
            var result = "";
            if ("".Equals(GetQueryString(strName)))
            {
                result = GetFormString(strName);
            }
            else
            {
                //strName.Replace("\"", "“");
                result = GetQueryString(strName);
            }
            if (!string.IsNullOrWhiteSpace(result))
            {
                return result;
            }
            return result;
        }


        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            try
            {
                return Utils.StrToInt(HttpContext.Current.Request.Query[strName], defValue);
            }
            catch
            {
                return defValue;
            }
        }


        /// <summary>
        /// 获得指定表单参数的int类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            try
            {
                return Utils.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) == defValue)
            {
                return GetFormInt(strName, defValue);
            }
            else
            {
                return GetQueryInt(strName, defValue);
            }
        }

        /// <summary>
        /// 获得指定Url参数的float类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static float GetQueryFloat(string strName, float defValue)
        {
            try
            {
                return Utils.StrToFloat(HttpContext.Current.Request.Query[strName], defValue);
            }
            catch
            {
                return defValue;
            }
        }


        /// <summary>
        /// 获得指定表单参数的float类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的float类型值</returns>
        public static float GetFormFloat(string strName, float defValue)
        {
            try
            {
                return Utils.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static float GetFloat(string strName, float defValue)
        {
            if (GetQueryFloat(strName, defValue) == defValue)
            {
                return GetFormFloat(strName, defValue);
            }
            else
            {
                return GetQueryFloat(strName, defValue);
            }
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            var result = string.Empty;

            try
            {
                result = HttpContext.Current.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = HttpContext.Current.Connection.RemoteIpAddress.ToString();
                }

                if (string.IsNullOrWhiteSpace(result) || !Utils.IsIP(result))
                {
                    return "0.0.0.0";
                }
            }
            catch (Exception)
            {
                result = "0.0.0.0";
            }

            return result;

        }

        /// <summary>
        /// 保存用户上传的文件
        /// </summary>
        /// <param name="path">保存路径</param>
        public static async System.Threading.Tasks.Task SaveRequestFileAsync(string path)
        {
            if (HttpContext.Current.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Current.Request.Form.Files[0];
                if (file.Length > 0)
                {
                    using (var inputStream = new FileStream(path, FileMode.Create))
                    {
                        // read file to stream
                        await file.CopyToAsync(inputStream);
                        // stream to byte array
                        byte[] array = new byte[inputStream.Length];
                        inputStream.Seek(0, SeekOrigin.Begin);
                        inputStream.Read(array, 0, array.Length);
                    }
                }
            }
        }

        public static async System.Threading.Tasks.Task<MemoryStream> GetRequestFileStreamAsync(string filename)
        {
            if (HttpContext.Current.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Current.Request.Form.Files[filename];
                if (file != null && file.Length > 0)
                {
                    var stream = new MemoryStream();
                    await file.CopyToAsync(stream);
                    return stream;
                    //using (var stream = new MemoryStream())
                    //{
                    //    await file.CopyToAsync(stream);
                    //    var sr = new StreamReader(stream);
                    //    return sr;
                    //}
                }
            }
            return new MemoryStream();
        }
        public static IFormFile GetRequestFileAsync(string filename)
        {
            if (HttpContext.Current.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Current.Request.Form.Files[filename];
                if (file != null && file.Length > 0)
                {
                    return file;
                }
            }
            return null;
        }


        #region 是否是手机设备登陆
        public static Boolean IsMobileDevice()
        {
            String[] mobileAgents = { "iphone", "android", "phone", "mobile", "wap", "netfront", "java", "opera mobi", "opera mini", "ucweb", "windows ce", "symbian", "series", "webos", "sony", "blackberry", "dopod", "nokia", "samsung", "palmsource", "xda", "pieplus", "meizu", "midp", "cldc", "motorola", "foma", "docomo", "up.browser", "up.link", "blazer", "helio", "hosin", "huawei", "novarra", "coolpad", "webos", "techfaith", "palmsource", "alcatel", "amoi", "ktouch", "nexian", "ericsson", "philips", "sagem", "wellcom", "bunjalloo", "maui", "smartphone", "iemobile", "spice", "bird", "zte-", "longcos", "pantech", "gionee", "portalmmm", "jig browser", "hiptop", "benq", "haier", "^lct", "320x320", "240x320", "176x220", "w3c ", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", "leno", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "oper", "palm", "pana", "pant", "phil", "play", "port", "prox", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-", "Googlebot-Mobile" };
            Boolean isMoblie = false;
            if (HttpContext.Current.Request.Headers["User-Agent"].ToString().ToLower() != null)
            {
                for (int i = 0; i < mobileAgents.Length; i++)
                {
                    if (HttpContext.Current.Request.Headers["User-Agent"].ToString().ToLower().IndexOf(mobileAgents[i]) >= 0)
                    {
                        isMoblie = true;
                        break;
                    }
                }
            }
            if (isMoblie)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }

}
