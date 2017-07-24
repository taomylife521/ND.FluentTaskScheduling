using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;
using ND.FluentTaskScheduling.CrawlKeGui.getModifyAndRefundStipulates;



/// <summary>
///_51bookHelper 接口访问帮助类
/// </summary>
public class _51bookHelper
{
    public _51bookHelper(string _agencyCode,string _safetyCode)
    {
        agencyCode = _agencyCode;
        safetyCode = _safetyCode;
    }
    //创建日记对象

    //公司代码
    private  static string agencyCode = "";
    //安全码
    private  static string safetyCode = "";  //正式账号

     

    

    

    #region 全取退改签规定 getModifyAndRefundStipulates(GetModifyAndRefundStipulatesRequest  model)
    //根据航空公司、舱位获取退改签规定
    public static object getModifyAndRefundStipulates(getModifyAndRefundStipulatesRequest model)
    {
        model.agencyCode = agencyCode;
        model.sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((agencyCode + model.lastSeatId + model.rowPerPage + safetyCode), "MD5").ToLower();
        try
        {
            getModifyAndRefundStipulatesReply result = new GetModifyAndRefundStipulatesServiceImpl_1_0Service().getModifyAndRefundStipulates(model);
            return result;
            
          
        }
        catch (Exception e)
        {
            return e.Message+"："+JsonConvert.SerializeObject(e);
        }
    }
    #endregion

   
}
