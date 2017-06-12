# 分布式任务调度平台
分布式任务调度平台产生历程

该平台主要用来解决公司内部一些定时调度任务的管理，避免出现这个任务在那个机子运行，或者那个任务在其它机子运行从而造成任务分散，机子分散，不便于管理和分析还有升级。

所以需要对任务，机子(以后统称节点)进行统一性的管理和操作。任务管理需要很好的进行任务添加，上传，预警，升级，日志查看，任务隔离，资源释放等相关的操作。而节点需要很好的进行添加，执行任务，故障转移，节点升级，节点监控，节点预警等一系列需要考虑到和解决的问题！

#分布式调度平台架构#
![整体架构图](http://files.cnblogs.com/files/taomylife/%E4%BB%BB%E5%8A%A1%E8%B0%83%E5%BA%A6%E7%B3%BB%E7%BB%9F%E6%9E%B6%E6%9E%84%E5%9B%BE.gif)
以上的分层，每个层代表一个项目,其中


1. 应用层:目前只实现了响应式Web端（项目路径:~/ND.Web/ND.FluentTaskScheduling.Web 涉及技术:adminlte,mvc）
2. 服务层:(项目路径:~/ND.Service/ND.FluentTaskScheduling.WebApi,涉及技术：ef,autofac，仓储模式,webapi,log4net)
3. windows服务层(也叫节点层，将来需作为windows服务部署):(项目路径:~/ND.NodeService/ND.FluentTaskScheduling.NodeService,涉及技术术语:AppDomain,Quartz.net,性能计数器,心跳,任务池，命令池，监控池，扫描器...)
## 分布式任务调度平台目前实现的功能列表 ##
1. 命令管理(命令列表(编辑，查看),命令队列列表(查询，查看日志),命令队列执行日志列表（查询）)
2. 任务管理(任务列表（添加，查询，开启，暂停，停止，查看日志，编辑）,任务管理日志列表（查询）)
3. 节点管理(节点列表（新增，查询，编辑，日志查看）,节点监控组件列表（查询）,节点日志列表（查询）)
4. 性能分析(任务性能分析(可视化查看cpu,内存，硬盘指标),节点性能分析（可视化查看可用内存，cpu，占用空间，io读写，iis请求）)
5. 用户管理(用户列表（新增，查询，编辑）)
6. 登录
##分布式任务调度平台部分截图##
![登录界面](http://files.cnblogs.com/files/taomylife/login.gif)
![统计页面](http://files.cnblogs.com/files/taomylife/%E7%BB%9F%E8%AE%A1%E9%A1%B5.gif)
![任务列表](http://files.cnblogs.com/files/taomylife/task.gif)








