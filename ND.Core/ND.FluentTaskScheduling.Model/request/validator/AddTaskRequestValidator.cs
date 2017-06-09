using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ND.FluentTaskScheduling.Model.enums;
//**********************************************************************
//
// 文件名称(File Name)：AddTaskRequestValidator.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-24 15:19:15         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-24 15:19:15          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request.validator
{
    public class AddTaskRequestValidator : ValidatorBase<AddTaskRequest>
    {
        public AddTaskRequestValidator()
        {
            RuleFor(x => x.AdminId).NotEmpty().WithMessage("后台创建人不能为空");

            RuleFor(x => x.IsEnabledAlarm).NotEmpty().WithMessage("是否报警不能为空");//.Matches(RegexValidator.OnlyZeroOrOne).WithMessage("是否报警格式不正确");  

            RuleFor(x => x.NodeId).GreaterThanOrEqualTo(0).WithMessage("节点不存在");

            //RuleFor(x=>x.TaskType).NotEmpty().WithMessage("任务类型不能为空").When(x=>x.TaskType == (int)TaskType.SchedulingTask, (c) =>
            //{
            //    RuleFor(c => c.TaskCorn).NotEmpty().WithMessage("调度任务Corn表达式不能为空");
            //})
        }
    }
}
