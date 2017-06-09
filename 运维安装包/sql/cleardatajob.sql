USE [msdb]
GO

/****** Object:  Job [清除日志（节点性能日志，命令队列执行日志,刷新队列日志）历史数据]    Script Date: 2017-05-25 17:44:11 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** Object:  JobCategory [REPL-History Cleanup]    Script Date: 2017-05-25 17:44:12 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'REPL-History Cleanup' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'REPL-History Cleanup'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'清除日志（节点性能日志，命令队列执行日志,刷新队列日志）历史数据', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=2, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'无描述。', 
		@category_name=N'REPL-History Cleanup', 
		@owner_login_name=N'sa', 
		@notify_email_operator_name=N'MrTao', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [1.清除3个小时之外的刷新队列数据]    Script Date: 2017-05-25 17:44:12 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'1.清除3个小时之外的刷新队列数据', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'delete FROM dbo.tb_refreshcommadqueuelog WHERE logcreatetime<DATEADD(hh,-3,GETDATE())', 
		@database_name=N'TaskSchedulingDB', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [2.清除3个小时外的运行中节点性能分析数据]    Script Date: 2017-05-25 17:44:12 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'2.清除3个小时外的运行中节点性能分析数据', 
		@step_id=2, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'DELETE  FROM dbo.tb_nodeperformance WHERE  lastupdatetime<DATEADD(hh,-3,GETDATE()) AND nodeid IN(SELECT id FROM dbo.tb_node WHERE nodestatus=1)', 
		@database_name=N'TaskSchedulingDB', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [3.清除3个小时外调度中的任务性能分析数据]    Script Date: 2017-05-25 17:44:13 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'3.清除3个小时外调度中的任务性能分析数据', 
		@step_id=3, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'delete FROM dbo.tb_performance WHERE  lastupdatetime<DATEADD(hh,-3,GETDATE()) AND taskid IN(SELECT id FROM dbo.tb_task WHERE taskschedulestatus=2)', 
		@database_name=N'TaskSchedulingDB', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'每隔半个小时清除日志（节点性能日志，命令队列执行日志,刷新队列日志）历史数据', 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=4, 
		@freq_subday_interval=30, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20170522, 
		@active_end_date=99991231, 
		@active_start_time=0, 
		@active_end_time=235959, 
		@schedule_uid=N'b974fcd6-d4e8-4203-8a58-0b00e20139d9'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

GO


