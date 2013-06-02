using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMS.Model
{
    [MetadataType(typeof(RequirementHistory_Validation))]
    public partial class RequirementHistory
    {
    }

    public class RequirementHistory_Validation{

        [Key]
        public System.Guid HistoryId { get; set; }

    }
}
