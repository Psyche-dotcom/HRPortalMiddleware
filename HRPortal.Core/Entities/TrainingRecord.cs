using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.Entities
{
    public class TrainingRecord : BaseEntity
    {
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public string TrainingName { get; set; }
        public string Provider { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Certificate { get; set; }
    }
}
