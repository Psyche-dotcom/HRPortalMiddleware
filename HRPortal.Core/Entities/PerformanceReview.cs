using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.Entities
{
    public class PerformanceReview : BaseEntity
    {
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Reviewer { get; set; }
        public string Comments { get; set; }
        public string Goals { get; set; }
    }
}
