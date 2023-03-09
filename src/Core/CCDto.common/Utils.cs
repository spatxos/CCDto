using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CCDto.common
{
    public class Utils
    {


        //#region Cookie
        //private static string cookieDomain = "";
        //public static void WriteCookie(string key, string strName, string strValue, int minute)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(key);
        //        cookie.Values[strName] = Utils.UrlEncode(strValue);
        //    }
        //    else
        //    {

        //        cookie.Values[strName] = Utils.UrlEncode(strValue);
        //        if (minute > 0)
        //        {
        //            cookie.Expires = DateTime.Now.AddMinutes(minute);
        //        }
        //    }
        //    if (cookieDomain != string.Empty)
        //        cookie.Domain = cookieDomain;
        //    HttpContext.Current.Response.AppendCookie(cookie);

        //}
        //public static void WriteCookieForViewNews(string strName, string strValue)
        //{
        //    Utils.WriteCookie("ViewNews", strName, strValue, 24 * 60);
        //}
        //public static string GetCookieForViewNews(string strName)
        //{
        //    return Utils.GetCookie("ViewNews", strName);
        //}
        //public static string GetCookie(string keyName, string strName)
        //{
        //    if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[keyName] != null && HttpContext.Current.Request.Cookies[keyName][strName] != null)
        //    {
        //        return Utils.UrlDecode(HttpContext.Current.Request.Cookies[keyName][strName].ToString());
        //    }

        //    return "";
        //}

        //#endregion

        //public static string SaveRequestFile()
        //{

        //    if (HttpContext.Current.Request.Files[0] != null && HttpContext.Current.Request.Files[0].FileName != "")
        //    {
        //        string filename = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);

        //        string fileextname = Path.GetExtension(filename).ToLower();
        //        string filetype = HttpContext.Current.Request.Files[0].ContentType.ToLower();
        //        //int filesize = HttpContext.Current.Request.Files[0].ContentLength;
        //        // 判断 文件扩展名/文件大小/文件类型 是否符合要求
        //        if ((fileextname.Equals(".jpg") || fileextname.Equals(".gif") || fileextname.Equals(".swf") || fileextname.Equals(".png")))
        //        {
        //            StringBuilder savedir = new StringBuilder();
        //            savedir.Append("../");
        //            savedir.Append("UpFiles/");

        //            savedir.Append(DateTime.Now.ToString("yyyy"));
        //            savedir.Append("/");
        //            savedir.Append(DateTime.Now.ToString("MM"));
        //            savedir.Append("/");
        //            savedir.Append(DateTime.Now.ToString("dd"));
        //            savedir.Append("/");

        //            string physical = Utils.GetMapPath(savedir.ToString());

        //            if (!Directory.Exists(physical))
        //            {
        //                Utils.CreateDir(physical);
        //                //System.IO.Directory.CreateDirectory(Utils.GetMapPath(savedir.ToString()));
        //            }
        //            Random random = new Random(unchecked((int)DateTime.Now.Ticks));
        //            string newfilename = (Environment.TickCount & int.MaxValue).ToString() + random.Next(1000, 9999).ToString() + fileextname; ;

        //            HttpContext.Current.Request.Files[0].SaveAs(Utils.GetMapPath(savedir + newfilename));
        //            return savedir + newfilename;
        //        }
        //    }
        //    return "";
        //}

        public static void SaveLog(string source, string logname = null)
        {
            if (!string.IsNullOrWhiteSpace(source))
            {
                var rootPath = "/log/";
                if (string.IsNullOrWhiteSpace(logname))
                {
                    logname = DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
                }
                if (!Directory.Exists("~" + rootPath))
                {
                    Directory.CreateDirectory("~" + rootPath);
                }
                if (!File.Exists("~" + rootPath + logname))
                {
                    File.Create("~" + rootPath + logname).Close();
                }
                File.AppendAllText("~" + rootPath + logname, $"\r\n{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {source}");
            }
        }
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

        }
        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string RemoveHtml(string Htmlstring)
        {
            string regexstr = @"<[^>]*>";
            Htmlstring = Regex.Replace(Htmlstring, regexstr, string.Empty, RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(ldquo|rdquo|mdash);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            return Htmlstring;
        }
        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            return TypeParse.IsNumericArray(strNumber);
        }

        #region Encoding
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        public static string Escape(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] ba = System.Text.Encoding.Unicode.GetBytes(s);
            for (int i = 0; i < ba.Length; i += 2)
            {
                sb.Append("%u");
                sb.Append(ba[i + 1].ToString("X2"));

                sb.Append(ba[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object Expression, int defValue)
        {

            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return Convert.ToInt32(str);
                    }
                }
            }
            return defValue;
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            return TypeParse.StrToFloat(strValue, defValue);
        }
        #endregion

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            return Encoding.UTF8.GetBytes(str).Length;
        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }

            }
            return -1;
        }


        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }
        public static bool ArrayIn(string[] stringarray, string str)
        {
            bool flag = false;
            foreach (string t in stringarray)
            {
                if (str.IndexOf(t, StringComparison.Ordinal) > -1)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }


                if (startIndex > str.Length)
                {
                    return "";
                }


            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            if (str.Length - startIndex < length)
            {
                length = str.Length - startIndex;
            }

            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }



        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        //public static string GetMapPath(string strPath)
        //{
        //    if (HttpContext.Current != null)
        //    {
        //        return HttpContext.Current.Server.MapPath(strPath);
        //    }
        //    else //非web程序引用
        //    {
        //        return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        //    }
        //}



        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }



        /// <summary>
        /// 以指定的ContentType输出指定文件文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的ContentType</param>
        //public static void ResponseFile(string filepath, string filename, string filetype)
        //{
        //    Stream iStream = null;

        //    // 缓冲区为10k
        //    byte[] buffer = new Byte[10000];

        //    // 文件长度
        //    int length;

        //    // 需要读的数据长度
        //    long dataToRead;

        //    try
        //    {
        //        // 打开文件
        //        iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


        //        // 需要读的数据长度
        //        dataToRead = iStream.Length;

        //        HttpContext.Current.Response.ContentType = filetype;
        //        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Utils.UrlEncode(filename.Trim()).Replace("+", " "));

        //        while (dataToRead > 0)
        //        {
        //            // 检查客户端是否还处于连接状态
        //            if (HttpContext.Current.Response.IsClientConnected)
        //            {
        //                length = iStream.Read(buffer, 0, 10000);
        //                HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
        //                HttpContext.Current.Response.Flush();
        //                buffer = new Byte[10000];
        //                dataToRead = dataToRead - length;
        //            }
        //            else
        //            {
        //                // 如果不再连接则跳出死循环
        //                dataToRead = -1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        HttpContext.Current.Response.Write("Error : " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (iStream != null)
        //        {
        //            // 关闭文件
        //            iStream.Close();
        //        }
        //    }
        //    HttpContext.Current.Response.End();
        //}

        /// <summary>
        /// 判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否可以直接显示</returns>
        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
            {
                return false;
            }
            string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
        }


        /// <summary>
        /// int型转换为string型
        /// </summary>
        /// <returns>转换后的string类型结果</returns>
        public static string IntToStr(int intValue)
        {
            //
            return Convert.ToString(intValue);
        }
        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        //public static string MD5(string str)
        //{
        //    byte[] b = Encoding.UTF8.GetBytes(str);
        //    b = new MD5CryptoServiceProvider().ComputeHash(b);
        //    string ret = "";
        //    for (int i = 0; i < b.Length; i++)
        //        ret += b[i].ToString("x").PadLeft(2, '0');
        //    return ret;
        //}

        public static Encoding ChineseHZEncoding { get; } = CodePagesEncodingProvider.Instance.GetEncoding(936);

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = ChineseHZEncoding.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        private static readonly uint[] s_lookup32 = Enumerable.Range(0, 255)
              .Select(i => {
                  string s = i.ToString("x2");
                  return ((uint)s[0]) + ((uint)s[1] << 16);
              }).ToArray();

        private static string ConvertToHex(byte[] bytes)
        {
            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                var val = s_lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }


        /// <summary>
        /// SHA256函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string SHA256(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
        }


        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }


        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {


            string myResult = p_SrcString;

            //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") ||
                System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
            {
                //当截取的起始位置超出字段串长度时
                if (p_StartIndex >= p_SrcString.Length)
                {
                    return "";
                }
                else
                {
                    return p_SrcString.Substring(p_StartIndex,
                                                   ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                }
            }


            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.UTF8.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }



                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {

                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                    {
                        nRealLength = p_Length + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }

        /// <summary>
        /// 自定义的替换字符串函数
        /// </summary>
        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }


        /// <summary>
        /// 生成指定数量的html空格符号
        /// </summary>
        public static string Spaces(int nSpaces)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < nSpaces; i++)
            {
                sb.Append(" &nbsp;&nbsp;");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {

            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }

        /// <summary>
        /// 清理字符串
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }

        /// <summary>
        /// 返回URL中结尾的文件名
        /// </summary>		
        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strs1 = url.Split(new char[] { '/' });
            return strs1[strs1.Length - 1].Split(new char[] { '?' })[0];
        }

        /// <summary>
        /// 根据阿拉伯数字返回月份的名称(可更改为某种语言)
        /// </summary>	
        public static string[] Monthes
        {
            get
            {
                return new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            }
        }

        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
            {
                return replacestr;
            }

            if (datetimestr.Equals(""))
            {
                return replacestr;
            }

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;

        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetWeekDay(DateTime dt)
        {
            var strWeek = dt.DayOfWeek;
            switch (strWeek)
            {
                case DayOfWeek.Monday:
                    return "周一";
                case DayOfWeek.Tuesday:
                    return "周二";
                case DayOfWeek.Wednesday:
                    return "周三";
                case DayOfWeek.Thursday:
                    return "周四";
                case DayOfWeek.Friday:
                    return "周五";
                case DayOfWeek.Saturday:
                    return "周六";
                case DayOfWeek.Sunday:
                    return "周日";
            }
            return "";
        }

        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 返回标准时间 
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {

                return fDateTime;
            }
            DateTime s = Convert.ToDateTime(fDateTime);
            return s.ToString(formatStr);
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        #region 返回本周日期
        /// <summary>
        /// 返回本周日期
        /// </summary>
        /// <returns></returns>
        public static DateTime GetWeekDate(bool isFirst = true)
        {
            var weeknow = Convert.ToInt32(System.DateTime.Now.DayOfWeek);
            //星期日 获取weeknow为0  
            weeknow = weeknow == 0 ? 7 : weeknow;
            var daydiff = (-1) * weeknow + 1;//本周第一天 
            var dayadd = 7 - weeknow;//本周最后一天  

            return DateTime.Now.AddDays(isFirst ? daydiff : dayadd);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }


        //public static string GetRealIP()
        //{
        //    string ip = DNTRequest.GetIP();

        //    return ip;
        //}

        /// <summary>
        /// 改正sql语句中的转义字符
        /// </summary>
        public static string mashSQL(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\'", "'");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// 替换sql语句中的有问题符号
        /// </summary>
        public static string ChkSQL(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("'", "''");
                //str = str.Replace("\"", "”");
                str2 = str;
            }
            return str2;
        }


        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int p_3)
        {
            string[] result = new string[p_3];

            string[] splited = SplitString(strContent, strSplit);

            for (int i = 0; i < p_3; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }
        /// <summary>
        /// 替换html字符
        /// </summary>
        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }



        //public static string ClearHtml(string strHtml)
        //{
        //    if (strHtml != "")
        //    {

        //        r = Regex.Replace(@"<\/?[^>]*>",RegexOptions.IgnoreCase);
        //        for (m = r.Match(strHtml); m.Success; m = m.NextMatch()) 
        //        {
        //            strHtml = strHtml.Replace(m.Groups[0].ToString(),"");
        //        }
        //    }
        //    return strHtml;
        //}


        /// <summary>
        /// 进行指定的替换(脏字过滤)
        /// </summary>
        public static string StrFilter(string str, string bantext)
        {
            string text1 = "";
            string text2 = "";
            string[] textArray1 = SplitString(bantext, "\r\n");
            for (int num1 = 0; num1 < textArray1.Length; num1++)
            {
                text1 = textArray1[num1].Substring(0, textArray1[num1].IndexOf("="));
                text2 = textArray1[num1].Substring(textArray1[num1].IndexOf("=") + 1);
                str = str.Replace(text1, text2);
            }
            return str;
        }



        /// <summary>
        /// 获得伪静态页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage)
        {
            int startPage = 1;
            int endPage = 1;

            string t1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            string t2 = "<a href=\"" + url + "-" + countPage + expname + "\">&raquo;</a>";

            if (countPage < 1) countPage = 1;
            if (extendPage < 3) extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span>");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append("<a href=\"");
                    s.Append(url);
                    s.Append("-");
                    s.Append(i);
                    s.Append(expname);
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            s.Append(t2);

            return s.ToString();
        }


        /// <summary>
        /// 获得帖子的伪静态页码显示链接
        /// </summary>
        /// <param name="expname"></param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetPostPageNumbers(int countPage, string url, string expname, int extendPage)
        {
            int startPage = 1;
            int endPage = 1;
            int curPage = 1;

            string t1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            string t2 = "<a href=\"" + url + "-" + countPage + expname + "\">&raquo;</a>";

            if (countPage < 1) countPage = 1;
            if (extendPage < 3) extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                s.Append("<a href=\"");
                s.Append(url);
                s.Append("-");
                s.Append(i);
                s.Append(expname);
                s.Append("\">");
                s.Append(i);
                s.Append("</a>");
            }
            s.Append(t2);

            return s.ToString();
        }



        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, "page");
        }

        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="pagetag">页码标记</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, pagetag, null);

        }

        /// <summary>
        /// 获得页码显示链接
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="url">超级链接地址</param>
        /// <param name="extendPage">周边页码显示个数上限</param>
        /// <param name="pagetag">页码标记</param>
        /// <param name="anchor">锚点</param>
        /// <returns>页码html</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag, string anchor)
        {
            if (pagetag == "")
                pagetag = "page";
            int startPage = 1;
            int endPage = 1;

            if (url.IndexOf("?") > 0)
            {
                url = url + "&";
            }
            else
            {
                url = url + "?";
            }

            string t1 = "<a href=\"" + url + "&" + pagetag + "=1";
            string t2 = "<a href=\"" + url + "&" + pagetag + "=" + countPage;
            if (anchor != null)
            {
                t1 += anchor;
                t2 += anchor;
            }
            t1 += "\">&laquo;</a>";
            t2 += "\">&raquo;</a>";

            if (countPage < 1)
                countPage = 1;
            if (extendPage < 3)
                extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span>");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append("<a href=\"");
                    s.Append(url);
                    s.Append(pagetag);
                    s.Append("=");
                    s.Append(i);
                    if (anchor != null)
                    {
                        s.Append(anchor);
                    }
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            s.Append(t2);

            return s.ToString();
        }

        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        //public static string HtmlEncode(string str)
        //{
        //    return HttpUtility.HtmlEncode(str);
        //}

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }


        /// <summary>
        /// 返回指定目录下的非 UTF8 字符集文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <returns>文件名的字符串数组</returns>
        public static string[] FindNoUTF8File(string Path)
        {
            //System.IO.StreamReader reader = null;
            StringBuilder filelist = new StringBuilder();
            DirectoryInfo Folder = new DirectoryInfo(Path);
            //System.IO.DirectoryInfo[] subFolders = Folder.GetDirectories(); 
            /*
            for (int i=0;i<subFolders.Length;i++) 
            { 
                FindNoUTF8File(subFolders[i].FullName); 
            }
            */
            FileInfo[] subFiles = Folder.GetFiles();
            for (int j = 0; j < subFiles.Length; j++)
            {
                if (subFiles[j].Extension.ToLower().Equals(".htm"))
                {
                    FileStream fs = new FileStream(subFiles[j].FullName, FileMode.Open, FileAccess.Read);
                    bool bUtf8 = IsUTF8(fs);
                    fs.Close();
                    if (!bUtf8)
                    {
                        filelist.Append(subFiles[j].FullName);
                        filelist.Append("\r\n");
                    }
                }
            }
            return Utils.SplitString(filelist.ToString(), "\r\n");

        }


        //0000 0000-0000 007F - 0xxxxxxx  (ascii converts to 1 octet!)
        //0000 0080-0000 07FF - 110xxxxx 10xxxxxx    ( 2 octet format)
        //0000 0800-0000 FFFF - 1110xxxx 10xxxxxx 10xxxxxx (3 octet format)

        /// <summary>
        /// 判断文件流是否为UTF8字符集
        /// </summary>
        /// <param name="sbInputStream">文件流</param>
        /// <returns>判断结果</returns>
        private static bool IsUTF8(FileStream sbInputStream)
        {
            int i;
            byte cOctets;  // octets to go in this UTF-8 encoded character 
            byte chr;
            bool bAllAscii = true;
            long iLen = sbInputStream.Length;

            cOctets = 0;
            for (i = 0; i < iLen; i++)
            {
                chr = (byte)sbInputStream.ReadByte();

                if ((chr & 0x80) != 0) bAllAscii = false;

                if (cOctets == 0)
                {
                    if (chr >= 0x80)
                    {
                        do
                        {
                            chr <<= 1;
                            cOctets++;
                        }
                        while ((chr & 0x80) != 0);

                        cOctets--;
                        if (cOctets == 0) return false;
                    }
                }
                else
                {
                    if ((chr & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    cOctets--;
                }
            }

            if (cOctets > 0)
            {
                return false;
            }

            if (bAllAscii)
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
            {
                return ((double)(bytes / 1073741824)).ToString("0") + "G";
            }
            if (bytes > 1048576)
            {
                return ((double)(bytes / 1048576)).ToString("0") + "M";
            }
            if (bytes > 1024)
            {
                return ((double)(bytes / 1024)).ToString("0") + "K";
            }
            return bytes.ToString() + "Bytes";
        }

        /// <summary>
        /// 将long型数值转换为Int32类型
        /// </summary>
        /// <param name="objNum"></param>
        /// <returns></returns>
        public static int SafeInt32(object objNum)
        {
            if (objNum == null)
            {
                return 0;
            }
            string strNum = objNum.ToString();
            if (IsNumeric(strNum))
            {

                if (strNum.ToString().Length > 9)
                {
                    if (strNum.StartsWith("-"))
                    {
                        return int.MinValue;
                    }
                    else
                    {
                        return int.MaxValue;
                    }
                }
                return Int32.Parse(strNum);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 返回相差的秒数
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string Time, int Sec)
        {
            TimeSpan ts = DateTime.Now - DateTime.Parse(Time).AddSeconds(Sec);
            if (ts.TotalSeconds > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalSeconds < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalSeconds;
        }

        /// <summary>
        /// 返回相差的分钟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (string.IsNullOrEmpty(time))
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddMinutes(minutes);
            if (ts.TotalMinutes > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalMinutes < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalMinutes;
        }

        /// <summary>
        /// 返回相差的小时数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (string.IsNullOrEmpty(time))
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddHours(hours);
            if (ts.TotalHours > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalHours < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalHours;
        }


        /// <summary>
        /// 为脚本替换特殊字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceStrToScript(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("'", "\\'");
            str = str.Replace("\"", "\\\"");
            return str;
        }


        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");

        }



        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {

            string[] userip = Utils.SplitString(ip, @".");
            for (int ipIndex = 0; ipIndex < iparray.Length; ipIndex++)
            {
                string[] tmpip = Utils.SplitString(iparray[ipIndex], @".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                    {
                        return true;
                    }

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                        {
                            r++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (r == 4)
                {
                    return true;
                }


            }
            return false;

        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        //public static void WriteCookie(string strName, string strValue)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie.Value = strValue;
        //    HttpContext.Current.Response.AppendCookie(cookie);

        //}
        ///// <summary>
        ///// 写cookie值
        ///// </summary>
        ///// <param name="strName">名称</param>
        ///// <param name="strValue">值</param>
        ///// <param name="strValue">过期时间(分钟)</param>
        //public static void WriteCookie(string strName, string strValue, int expires)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie.Value = strValue;
        //    cookie.Expires = DateTime.Now.AddMinutes(expires);
        //    HttpContext.Current.Response.AppendCookie(cookie);

        //}

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        //public static string GetCookie(string strName)
        //{
        //    if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
        //    {
        //        return HttpContext.Current.Request.Cookies[strName].Value.ToString();
        //    }

        //    return "";
        //}

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }


        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }



        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            return TypeParse.IsNumeric(Expression);
        }
        /// <summary>
        /// 从HTML中获取文本,保留br,p,img
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static string GetTextFromHTML(string HTML)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(@"</?(?!br|/?p|img|b|strong|div)[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return regEx.Replace(HTML, "");
        }

        public static bool IsDouble(object Expression)
        {
            return TypeParse.IsDouble(Expression);
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object Expression, bool defValue)
        {
            return TypeParse.StrToBool(Expression, defValue);
        }


        public static string AdDeTime(int times)
        {
            string newtime = (DateTime.Now).AddMinutes(times).ToString();
            return newtime;

        }
        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {

            return Regex.IsMatch(str, @"^[0-9]*$");
        }

        public static bool IsRuleTip(Hashtable NewHash, string ruletype, out string key)
        {
            key = "";
            foreach (DictionaryEntry str in NewHash)
            {

                try
                {
                    string[] single = SplitString(str.Value.ToString(), "\r\n");

                    foreach (string strs in single)
                    {
                        if (strs != "")


                            switch (ruletype.Trim().ToLower())
                            {
                                case "email":
                                    if (IsValidDoEmail(strs.ToString()) == false)
                                        throw new Exception();
                                    break;

                                case "ip":
                                    if (IsIPSect(strs.ToString()) == false)
                                        throw new Exception();
                                    break;

                                case "timesect":
                                    string[] splitetime = strs.Split('-');
                                    if (Utils.IsTime(splitetime[1].ToString()) == false || Utils.IsTime(splitetime[0].ToString()) == false)
                                        throw new Exception();
                                    break;

                            }

                    }


                }
                catch
                {
                    key = str.Key.ToString();
                    return false;
                }
            }
            return true;

        }

        /// <summary>
        /// 删除最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearLastChar(string str)
        {
            if (str == "")
                return "";
            else
                return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && System.IO.File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }


        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    else
                    {
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                    }
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }


        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        /// 将字符串转换为Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToColor(string color)
        {
            int red, green, blue = 0;
            char[] rgb;
            color = color.TrimStart('#');
            color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
            switch (color.Length)
            {
                case 3:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
                    green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
                    blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                case 6:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
                    green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
                    blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                default:
                    return Color.FromName(color);

            }
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>创建是否成功</returns>
        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);

        /// <summary>
        /// 建立文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CreateDir(string name)
        {
            return Utils.MakeSureDirectoryPathExists(name);
        }

        public static System.Data.DataTable GetPagedTable(System.Data.DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Copy();
            newdt.Clear();

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }
        //public static object CopyClass(object obj)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        stream.Position = 0L;
        //        formatter.Serialize(stream, obj);
        //        stream.Position = 0L;
        //        return formatter.Deserialize(stream);
        //    }
        //}
        public static string HtmlEncode(string str)
        {
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br />");
            str = str.Replace("'", "\"");
            return str;
        }

        /// <summary>
        /// 根据Url获得源文件内容
        /// </summary>
        /// <param name="url">合法的Url地址</param>
        /// <returns></returns>
        public static string GetSourceTextByUrl(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = 20000;//20秒超时
                WebResponse response = request.GetResponse();

                Stream resStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.GetEncoding("gb2312"));
                return sr.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }

        public static string GetDir(int num, int per)
        {
            if (num < per)
            {
                return "/";
            }
            string path = "/";
            while (true)
            {
                path += (num % per).ToString() + "/";
                num = num / per;
                if (num < per)
                {
                    path += num.ToString() + "/";
                    break;
                }
            }
            return path;
        }

        /// <summary>
        /// 过滤非法信息词
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string FilterSexWord(string strContent)
        {
            string resultString = null;
            try
            {
                resultString = Regex.Replace(strContent, "(肛交|口交|蜜穴|淫水|轮奸|迷奸|裸聊|成人小说|激情自拍|性吧|乳交|猛插|穴穴|抽插|阴户|兽交|淫叫|舔阴|肥穴|狂插|性爱文学|肉穴|鸡巴|密穴|舔弄|吸啜)", "**", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            catch (ArgumentException)
            {
                // Syntax error in the regular expression
            }
            return resultString;
        }


        #region WriteTextFile
        private static Encoding defaultEncoding = Encoding.UTF8;
        /// <summary>
        /// WriteTextFile
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="text">文本内容</param>
        public static void WriteTextFile(string fileName, string text)
        {
            WriteTextFile(fileName, text, false, true, defaultEncoding);
        }

        /// <summary>
        /// WriteTextFile
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="text">文本内容</param>
        /// <param name="encoding">编码</param>	
        public static void WriteTextFile(string fileName, string text, Encoding encoding)
        {
            WriteTextFile(fileName, text, false, true, encoding);
        }

        /// <summary>
        /// WriteTextFile
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="text">文本内容</param>
        /// <param name="createDir">是否创建目录</param>		
        public static void WriteTextFile(string fileName, string text, bool createDir)
        {
            WriteTextFile(fileName, text, false, createDir, defaultEncoding);
        }

        /// <summary>
        /// WriteTextFile
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="text">文本内容</param>
        /// <param name="createDir">是否创建目录</param>
        /// <param name="encoding">编码</param>	
        public static void WriteTextFile(string fileName, string text, bool createDir, Encoding encoding)
        {
            WriteTextFile(fileName, text, false, createDir, encoding);
        }

        /// <summary>
        /// WriteTextFile
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="text">文本内容</param>
        /// <param name="append">是否添加到文本后面</param>
        /// <param name="createDir">是否创建目录</param>
        /// <param name="encoding">编码</param>
        public static void WriteTextFile(string fileName, string text, bool append, bool createDir, Encoding encoding)
        {
            if (createDir)
            {
                string dirName = Path.GetDirectoryName(fileName);

                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
            }

            using (StreamWriter sw = new StreamWriter(fileName, append, encoding))
            {
                sw.Write(text);
            }
        }

        #endregion

        #region ReadTextFile
        /// <summary>
        /// ReadTextFile
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>文本内容</returns>
        public static string ReadTextFile(string fileName)
        {
            return ReadTextFile(fileName, defaultEncoding);
        }

        /// <summary>
        /// ReadTextFile
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="encoding">编码</param>
        /// <returns>文本内容</returns>
        public static string ReadTextFile(string fileName, Encoding encoding)
        {
            string text = "";

            try
            {
                using (StreamReader sr = new StreamReader(fileName, encoding))
                {
                    text = sr.ReadToEnd();
                }
            }
            catch (System.Exception)
            {

            }

            return text;
        }


        #endregion

        /// <summary>
        /// Sha1
        /// </summary>
        /// <param name="orgStr"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Sha1(string orgStr, string encode = "UTF-8")
        {
            var sha1 = new SHA1Managed();
            var sha1bytes = System.Text.Encoding.GetEncoding(encode).GetBytes(orgStr);
            byte[] resultHash = sha1.ComputeHash(sha1bytes);
            string sha1String = BitConverter.ToString(resultHash).ToLower();
            sha1String = sha1String.Replace("-", "");
            return sha1String;
        }
        public static string MD5(string encypStr, string charset = "UTF-8")
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        private static string[] strs = new string[]
                                 {
                                  "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                  "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
                                 };
        /// <summary>
        /// 创建随机字符串
        /// </summary>
        /// <returns></returns>
        public static string CreateNonce_str()
        {
            Random r = new Random();
            var sb = new StringBuilder();
            var length = strs.Length;
            for (int i = 0; i < 15; i++)
            {
                sb.Append(strs[r.Next(length - 1)]);
            }
            return sb.ToString();
        }


        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        private static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        /// <summary>
        /// html转义
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        public static string Transfer(string instr)
        {
            if (instr == null) return "";
            return instr.Replace("&", "&amp;").Replace("<", "&lt;")
                        .Replace(">", "&gt;").Replace("\"", "&quot;");
        }
        /// <summary>
        /// html转义字符串还原
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        public static string DeTransfer(string instr)
        {
            if (instr == null) return "";
            return instr.Replace("&amp;", "&").Replace("&lt;", "<")
                        .Replace("&gt;", ">").Replace("&quot;", "\"");
        }
        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <returns></returns>
        public static string HttpRequestPost(string url, Dictionary<string, string> fileDictionary = null, string encoding = "UTF8")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = 100;

            var postStream = new MemoryStream();
            #region 处理Form表单文件上传
            var formUploadFile = fileDictionary != null && fileDictionary.Count > 0;//是否用Form上传文件
            if (formUploadFile)
            {
                //通过表单上传文件
                string boundary = "----" + DateTime.Now.Ticks.ToString("x");

                string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";

                foreach (var file in fileDictionary)
                {
                    try
                    {
                        var fileName = file.Value;
                        //准备文件流
                        using (var fileStream = new FileStream(fileName, FileMode.Open))
                        {
                            var formdata = string.Format(formdataTemplate, file.Key, fileName);
                            var formdataBytes = Encoding.ASCII.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行
                            postStream.Write(formdataBytes, 0, formdataBytes.Length);

                            //写入文件
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                //结尾
                var footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;

                //直接写入流
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                postStream.Close();//关闭文件访问
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding(encoding)))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        public static void Download(string url, Stream stream)
        {
            WebClient wc = new WebClient();
            var data = wc.DownloadData(url);
            foreach (var b in data)
            {
                stream.WriteByte(b);
            }
        }

        /** 对字符串进行URL编码 */
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception)
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;
            }
        }


        /** 对字符串进行URL解码 */
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception)
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;

            }
        }
    }

}
