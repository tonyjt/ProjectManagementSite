using MVCExtension;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using CustomExtension;
using System.ComponentModel.DataAnnotations;
namespace PMS.PMSSite.Models
{
    public class TaskIndexModel : PagerModel,IPagerModel
    {
        public string Requirement { get; set; }

        public string Version { get; set; }

        public string User { get; set; }

        public TaskStatusModel? Status { get; set; }

        public RoleEnum? Role { get; set; }

        public new NameValueCollection GetRequestParameters()
        {
                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("version", Version);
                parameters.Add("requirement", Requirement);
                parameters.Add("user", User);
                if(Status.HasValue)
                    parameters.Add("status", Status.GetByteEnumValueToString());
                if (Role.HasValue)
                    parameters.Add("role", Role.GetByteEnumValueToString());

                return parameters;
         
        }

        public IEnumerable<ProjectTask> Tasks { get; set; }

        public IEnumerable<ProjectVersion> Versions { get; set; }

        public IEnumerable<Requirement> Requirements { get; set; }

        public IEnumerable<TaskStatusModel> Statuses { get; set; }

        public IEnumerable<ProjectParticipator> Users{get;set;}

        public IEnumerable<RoleEnum> Roles { get; set; }

        public TaskIndexModel()
        {
            this.PageSize = 5;
        }

        public RoleEnum UserRole { get; set; }
    }

    public class TaskDetailModel:TaskDetailPostModel
    {
        public ProjectTask Item { get; set; }

        public IEnumerable<Requirement> Requirements { get; set; }

        public IEnumerable<ProjectParticipator> Paticipators { get; set; }

        public bool IsNew { get; set; }
    }

    public class TaskDetailPostModel
    {
        public Guid RequirementId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public bool NeedDesigner { get; set; }

        public bool NeedDeveloper { get; set; }

        public bool NeedTester { get; set; }

        public bool NeedOperator { get; set; }

        public Guid DesignerId { get; set; }

        public Guid DeveloperId { get; set; }

        public Guid TesterId { get; set; }

        public Guid OperatorId { get; set; }

        public bool Continue { get; set; }
    }

    public class TaskModel
    {
        public ProjectTask Task { get; set; }

        public IEnumerable<ProjectParticipator> Users { get; set; }

        public RoleEnum UserRole { get; set; }
    }

    public class TaskRolePartialModel
    {
        public ProjectTask Task { get; set; }

        public IEnumerable<ProjectParticipator> Users { get; set; }

        public RoleEnum Role { get; set; }

        public bool UserEnable { get; set; }

        public TaskRolePartialModel(TaskModel taskModel,RoleEnum role)
        {
            this.Task = taskModel.Task;
            this.Users = taskModel.Users;
            this.Role = role;
            UserEnable = (taskModel.UserRole & role) != 0;
        }
    }

    public enum TaskStatusModel: byte
    {
        [Display(Name="需分配")]
        NeedAssign = 0,

        [Display(Name = "已分配")]
        Finishing,

        [Display(Name="未完成")]
        UnFinished,

        [Display(Name = "设计完成")]
        DesignFinish,

        [Display(Name = "需要测试")]
        NeedTest,

        [Display(Name = "需要部署")]
        NeedDeploy,

        [Display(Name = "已完成")]
        Finished,

        [Display(Name="已取消")]
        Cacneled,
    }


}