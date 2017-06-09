using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.FluentTaskScheduling.Core.CommandSet;

//**********************************************************************
//
// 文件名称(File Name)：DbCommandQueueScan.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 14:36:48         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 14:36:48          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
    public class DbCommandQueueScan : AbsCommandQueueScan
    {
        ///// <summary>
        ///// 上一次日志扫描的最大id
        ///// </summary>
        //public static int lastMaxID = -1;

        #region 开启任务扫描
        /// <summary>
        /// 开启任务扫描
        /// </summary>
        /// <returns></returns>
        public override bool StartScanCommandQueue()
        {
            //1.扫描分配给该节点的任务命令（读库或者直接读webapi）
            //2.遍历命令集合，并从命令池中找寻命令并执行，执行成功，更新命令队列，执行失败也更新命令状态
            var r = NodeProxy.PostToServer<LoadCommandQueueListResponse, LoadCommandQueueListRequest>(ProxyUrl.LoadCommandQueueList_Url, new LoadCommandQueueListRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, LastMaxId = GlobalNodeConfig.NodeInfo.lastmaxid, CommandStatus = (int)Model.enums.ExecuteStatus.NoExecute });
            if (r.Status != ResponesStatus.Success)
            {
                onScanMsg("读取命令失败,服务器返回:" + JsonConvert.SerializeObject(r));
                return false;
            }
            int sucess = 0;
            int failed = 0;
            List<string> cmdQueueIdList = new List<string>();
            r.Data.CommandQueueList.ForEach(x=>cmdQueueIdList.Add(x.id.ToString()));
            if (r.Data.CommandQueueList.Count > 0)
            {
                onScanMsg("当前节点扫描到" + r.Data.CommandQueueList.Count + "条命令("+string.Join(",",cmdQueueIdList.ToArray())+"),并执行中");
            }
            
            foreach (var item in r.Data.CommandQueueList)
            {
                try
                {
                    GlobalNodeConfig.NodeInfo.lastmaxid = item.id > GlobalNodeConfig.NodeInfo.lastmaxid ? item.id : GlobalNodeConfig.NodeInfo.lastmaxid;
                    CommandPoolManager.CreateInstance().Get(item.commanddetailid.ToString()).commandBase.CommandQueue = item;
                    CommandPoolManager.CreateInstance().Get(item.commanddetailid.ToString()).commandBase.TryExecute();
                   
                    sucess += 1;
                    //更新命令状态为成功
                    onScanMsg(string.Format("当前节点执行命令成功! id:{0},命令名:{1},命令参数:{2}", item.id, item.commandmainclassname, item.commandparams));

                }
                catch (Exception ex)
                {
                    failed += 1;
                    //添加任务执行失败日志
                    onScanMsg("执行节点命令异常:任务id:" + item.taskid + ",命令名称:" + item.commandmainclassname + "(" + item.id + ")", ex);

                }

            }
            onScanMsg("当前循环扫描命令队列完成,保存上次最大命令队列id值:" + GlobalNodeConfig.NodeInfo.lastmaxid + ",扫描到命令个数:" + r.Data.CommandQueueList.Count + ",执行成功个数:" + sucess + ",执行失败个数:" + failed);
            return true;
        } 
        #endregion
    }
}
