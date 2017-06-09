using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Command.CommandHandler;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandBuilder.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-05 17:20:11         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-05 17:20:11          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
   public class CommandPoolBuilder:ICommandPoolBuilder
    {
       public event EventHandler<CommandEventArgs> OnInitEvent;

       #region 封装事件
       private void onInit(string msg, Exception ex = null)
       {
           if (OnInitEvent != null)
           {
               OnInitEvent(this, new CommandEventArgs() { msg = msg, ex = ex });
           }
       } 
       #endregion

       #region BuildCommandPool
       /// <summary>
       /// 构建命令池
       /// </summary>
       public void BuildCommandPool()
       {
           try
           {
               //读取库中命令，然后反射创建对象，封装命令运行时信息，并添加到命令池中
               var r = NodeProxy.PostToServer<LoadCommandListResponse, LoadCommandListRequest>(ProxyUrl.LoadCommandList_Url, new LoadCommandListRequest() { NodeId = GlobalNodeConfig.NodeID.ToString(), Source = Source.Node });
               if (r.Status != ResponesStatus.Success)
               {
                   onInit("读取命令失败,服务器返回:" + JsonConvert.SerializeObject(r));
                   return;
               }
               if (r.Data.CommandLibDetailList.Count <= 0)
               {
                   onInit("读取到0条命令,无法构建命令池");
                   return;
               }
               int commandCount = r.Data.CommandLibDetailList.Count;

               onInit("读取到" + commandCount.ToString() + "条命令");
               onInit("开始创建命令对象,并添加到命令池");
               int sucess = 0;
               int failed = 0;
               foreach (var item in r.Data.CommandLibDetailList)
               {
                   try
                   {
                       var cmdruntime = CommandFactory.Create(item);
                       if (cmdruntime != null)
                       {
                           CommandPoolManager.CreateInstance().Add(item.id.ToString(), cmdruntime);
                           sucess += 1;
                       }
                       else
                       {
                           onInit("命令:" + item.commandname + "(" + item.id.ToString() + "),添加到命令池失败!创建命令对象为null");
                           failed += 1;
                       }

                   }
                   catch (Exception ex)
                   {
                       onInit("命令:" + item.commandname + "(" + item.id.ToString() + "),添加到命令池失败!", ex);
                       failed += 1;
                   }
               }
               onInit("构建命令池完成,命令总数量:" + commandCount + ",成功数量:" + sucess.ToString() + ",失败数量:" + failed.ToString());
           }
           catch (Exception ex)
           {
               onInit("命令池构建失败：" + ex.Message, ex);
           }
       } 
       #endregion





      
    }
}
