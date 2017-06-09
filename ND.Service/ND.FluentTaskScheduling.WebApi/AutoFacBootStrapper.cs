using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FluentValidation;
using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.WebApi.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
//**********************************************************************
//
// 文件名称(File Name)：AutoFacBootStrapper.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 14:30:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 14:30:36          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.WebApi
{
    public class AutoFacBootStrapper
    {
        public static void CoreAutoFacInit()
        {
            var builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;

            SetupResolveRules(builder);

            ////注册所有的Controllers
            //builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //注册所有的ApiControllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            var container = builder.Build();
            //注册api容器需要使用HttpConfiguration对象
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            INodeRepository node = container.Resolve<INodeRepository>();
            IUserRepository user = container.Resolve<IUserRepository>();
            ITaskRepository task = container.Resolve<ITaskRepository>();
            ITaskVersionRepository taskversion = container.Resolve<ITaskVersionRepository>();
            INodeMonitorRepository nodemonitor = container.Resolve<INodeMonitorRepository>();
            GlobalConfig.MonitorList.Add(new NodeHeartBeatMonitor(node, user, task, taskversion));
            GlobalConfig.MonitorList.Add(new MonitorPluginMonitor(node, user, nodemonitor));
        }

           

        private static void SetupResolveRules(ContainerBuilder builder)
        {
            //WebAPI只用引用services和repository的接口，不用引用实现的dll。
            //如需加载实现的程序集，将dll拷贝到bin目录下即可，不用引用dll
          
            var iRepository = Assembly.Load("ND.FluentTaskScheduling.Domain");
            var repository = Assembly.Load("ND.FluentTaskScheduling.Repository");
            //AssemblyScanner
            //     .FindValidatorsInAssemblyContaining<ValidatorFactoryBase>()
            //    .ForEach(x => builder.RegisterType(x.ValidatorType).As(x.InterfaceType).SingleInstance());
             
            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(iRepository, repository)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces();
             //builder.RegisterAssemblyTypes(IValidator, AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).ToList())
             // .Where(t => t.Name.EndsWith("Validator"))
             // .AsImplementedInterfaces();
        }
    }
}