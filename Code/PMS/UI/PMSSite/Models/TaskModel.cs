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

        public new NameValueCollection GetRequestParameters()
        {
                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("version", Version);
                parameters.Add("requirement", Requirement);
                parameters.Add("user", User);
                if(Status.HasValue)
                    parameters.Add("status", Status.GetByteEnumValueToString());
                

                return parameters;
         
        }

        public IEnumerable<ProjectTask> Tasks { get; set; }

        public IEnumerable<ProjectVersion> Versions { get; set; }

        public IEnumerable<Requirement> Requirements { get; set; }

        public IEnumerable<TaskStatusModel> Statuses { get; set; }

        public IEnumerable<ProjectParticipator> Users{get;set;}

        public TaskIndexModel()
        {
            this.PageSize = 5;
        }
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

    public class TaskRolePartialModel
    {
        public ProjectTask Task { get; set; }

        public IEnumerable<ProjectParticipator> Users { get; set; }

        public RoleEnum Role { get; set; }

    }

    public enum TaskStatusModel: byte
    {
        [Display(Name="需要分配")]
        NeedAssign = 0,

        [Display(Name = "已分配")]
        Finishing = 1,

        [Display(Name = "已完成")]
        Finished = 2,

        [Display(Name="已取消")]
        Cacneled = 3
    }


}