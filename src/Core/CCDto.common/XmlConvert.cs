using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CCDto.common
{
    public class XmlConvert
    {
        //序列化对象成xml字符串
        public static string SerializeObject(object myObj)
        {
            var xmlStr = string.Empty;
            if (myObj != null)
            {
                XmlSerializer xs = new XmlSerializer(myObj.GetType());
                using (var stringWriter = new StringWriter())
                {
                    xs.Serialize(stringWriter, myObj);
                    xmlStr = stringWriter + "";
                }
            }
            return xmlStr;
        }

        //xml反序列化成对象
        public static T DeserializeObject<T>(string xml) where T : new()
        {
            T t = default(T);
            if (!string.IsNullOrEmpty(xml))
            {
                t = new T();
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                StringReader reader = new StringReader(xml);//将xml字符串转换成stream
                t = (T)xs.Deserialize(reader);
                reader.Close();
            }
            return t;
        }
    }
}
