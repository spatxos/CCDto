using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using CCDto.entity.Base;

namespace CCDto.common
{

    public static class HttpHelper
    {
        #region 获取请求数据
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="method">post/get</param>
        /// <param name="param">ie=utf-8&source=txt&query=hello&t=1327829764203&token=8a7dcbacb3ed72cad9f3fb079809a127&from=auto&to=auto</param>
        /// <returns></returns>
        public static ReturnMsg RequestUrl(string url, string param, string method = "post", string contentype = "application/json")
        {
            var returnMsg = new ReturnMsg();
            //returnMsg.IsSuccess = true;
            //returnMsg.Message = @"{
            //                          'code': '0',
            //                          'hardwareCode':'12345678',
            //                          'message': '0', 
            //                      }";
            //return returnMsg;
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                string res = string.Empty;
                //请求
                WebRequest webRequest = null;
                Stream postStream = null;

                //响应
                WebResponse webResponse = null;
                StreamReader streamReader = null;

                //请求
                webRequest = WebRequest.Create(url);
                webRequest.Proxy = new WebProxy();
                webRequest.Method = method;
                webRequest.Timeout = 10000;
                webRequest.ContentType = contentype;
                if (method.ToLower() != "get")
                {
                    //向请求流写数据
                    byte[] postData = encoding.GetBytes(param);
                    webRequest.ContentLength = postData.Length;
                    postStream = webRequest.GetRequestStream();
                    postStream.Write(postData, 0, postData.Length);
                    //必须加上以下代码
                    postStream.Flush();
                    postStream.Close();
                }
                //响应
                webResponse = webRequest.GetResponse();//1、此处在wince平台上报“在写入请求数据前，不能检索此请求的响应”
                streamReader = new StreamReader(webResponse.GetResponseStream(), encoding);
                res = streamReader.ReadToEnd();
                returnMsg.IsSuccess = true;
                returnMsg.Message = res;
                streamReader.Close();
                return returnMsg;
            }
            catch (Exception e)
            {
                returnMsg.Message = e.Message;
                return returnMsg;
            }
        }
        #endregion
    }
}
