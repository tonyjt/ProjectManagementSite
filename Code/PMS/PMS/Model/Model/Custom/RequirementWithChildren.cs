using PMS.Tool.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class RequirementWithChildren
    {
        public Requirement Requirement { get; set; }

        public IEnumerable<RequirementWithChildren> Children { get; set; }

        public bool IsLeafNode
        {
            get {
                return GuidHelper.IsValid(Requirement.ParentId) &&( Children == null || Children.Count() == 0);
            }
        }
    }
}
