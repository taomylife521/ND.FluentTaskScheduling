using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

//**********************************************************************
//
// 文件名称(File Name)：HttpClientHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 14:57:26         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 14:57:26          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
    public class HttpClientHelper
    {
        /// <summary>
         /// get请求
         /// </summary>
         /// <param name="url"></param>
         /// <returns></returns>
         public static string GetResponse(string url)
         {
             HttpClient httpClient = new HttpClient();
             try
             {
                 if (url.StartsWith("https"))
                     System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;


                 httpClient.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/json"));
                 HttpResponseMessage response = httpClient.GetAsync(url).Result;

                 if (response.IsSuccessStatusCode)
                 {
                     string result = response.Content.ReadAsStringAsync().Result;
                     return result;
                 }
                 return null;
             }
             catch (Exception ex)
             {
                 return null;
             }
             finally
             {
                 httpClient.Dispose();
             }

         }
 
         public static T GetResponse<T>(string url)
             where T : class,new()
         {
             if (url.StartsWith("https"))
                 System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
 
             HttpClient httpClient = new HttpClient();
             httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
             HttpResponseMessage response = httpClient.GetAsync(url).Result;
 
             T result = default(T);
 
             if (response.IsSuccessStatusCode)
             {
                 Task<string> t = response.Content.ReadAsStringAsync();
                 string s = t.Result;
 
                 result = JsonConvert.DeserializeObject<T>(s);
             }
             return result;
         }
 
         /// <summary>
         /// post请求
         /// </summary>
         /// <param name="url"></param>
         /// <param name="postData">post数据</param>
         /// <returns></returns>
         public static string PostResponse(string url, string postData)
         {
             if (url.StartsWith("https"))
                 System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
 
             HttpContent httpContent = new StringContent(postData);
             httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
             HttpClient httpClient = new HttpClient();
 
             HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
 
             if (response.IsSuccessStatusCode)
             {
                 string result = response.Content.ReadAsStringAsync().Result;
                 return result;
             }
             return null;
         }
 
         /// <summary>
         /// 发起post请求
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="url">url</param>
         /// <param name="postData">post数据</param>
         /// <returns></returns>
         public static T PostResponse<T>(string url, string postData)
             where T : class,new()
         {
             if (url.StartsWith("https"))
                 System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
 
             HttpContent httpContent = new StringContent(postData);
             httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
             HttpClient httpClient = new HttpClient();
 
             T result = default(T);
 
             HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
 
             if (response.IsSuccessStatusCode)
             {
                 Task<string> t = response.Content.ReadAsStringAsync();
                 string s = t.Result;
 
                 result = JsonConvert.DeserializeObject<T>(s);
             }
             return result;
         }
 
         /// <summary>
         /// V3接口全部为Xml形式，故有此方法
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="url"></param>
         /// <param name="xmlString"></param>
         /// <returns></returns>
         public static T PostXmlResponse<T>(string url, string xmlString)
             where T : class,new()
         {
             if (url.StartsWith("https"))
                 System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
 
             HttpContent httpContent = new StringContent(xmlString);
             httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
             HttpClient httpClient = new HttpClient();
 
             T result = default(T);
 
             HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
 
             if (response.IsSuccessStatusCode)
             {
                 Task<string> t = response.Content.ReadAsStringAsync();
                 string s = t.Result;
 
                 result = XmlDeserialize<T>(s);
             }
             return result;
         }
 
         /// <summary>
         /// 反序列化Xml
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="xmlString"></param>
         /// <returns></returns>
         public static T XmlDeserialize<T>(string xmlString) 
             where T : class,new ()
         {
             try
             {
                 XmlSerializer ser = new XmlSerializer(typeof(T));
                 using (StringReader reader = new StringReader(xmlString))
                 {
                     return (T)ser.Deserialize(reader);
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception("XmlDeserialize发生异常：xmlString:" + xmlString + "异常信息：" + ex.Message);
             }
 
         }
    }
}
