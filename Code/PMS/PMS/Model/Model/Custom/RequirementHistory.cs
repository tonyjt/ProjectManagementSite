using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMS.Model
{
    [MetadataType(typeof(RequirementHistory_Validation))]
    public partial class RequirementHistory
    {
        public RequirementHistory()
        {
        }
        public RequirementHistory(Requirement requirement)
        {
            RequirementId = requirement.RequirementId;
            VersionId = requirement.VersionId;
            ParentId = requirement.ParentId;
            Title = requirement.Title;
            Content = requirement.Content;
            CreateDate = requirement.UpdateTime;
            HistoryId = Guid.NewGuid();
        }
    }

    public class RequirementHistory_Validation{

        [Key]
        public System.Guid HistoryId { get; set; }

    }
}
