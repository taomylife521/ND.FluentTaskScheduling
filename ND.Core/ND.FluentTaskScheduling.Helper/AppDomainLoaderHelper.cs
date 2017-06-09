using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AppDomainLoaderHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-13 17:22:35         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-13 17:22:35          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
    public class AppDomainLoaderHelper<T> where T:class
    {
        /// <summary>
        /// 加载应用程序域,获取相应实例
        /// </summary>
        /// <param name="dllPath"></param>
        /// <param name="classPath"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public T CreateDomain(string dllPath,string classPath,out AppDomain domain)
        {
            try
            {
                AppDomainSetup setup = new AppDomainSetup();
                if (File.Exists(dllPath + ".config"))
                {
                    setup.ConfigurationFile = dllPath + ".config";
                }
                setup.ShadowCopyFiles = "true";
                setup.ApplicationBase = Path.GetDirectoryName(dllPath);
                domain = AppDomain.CreateDomain(Path.GetFileName(dllPath).Replace(".dll","").Replace(".exe",""), null, setup);
                AppDomain.MonitoringIsEnabled = true;
                T obj = (T)domain.CreateInstanceFromAndUnwrap(dllPath, classPath);
                return obj;
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                domain = null;
                return null;
            }
        }

        /// <summary>
        /// 卸载应用程序域
        /// </summary>
        /// <param name="domain"></param>
        public void UnLoad(AppDomain domain)
        {
            AppDomain.Unload(domain);
            domain = null;
        }
    }
}
