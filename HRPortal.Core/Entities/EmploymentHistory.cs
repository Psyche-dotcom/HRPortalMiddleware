using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.Entities
{
    public class EmploymentHistory : BaseEntity
    {
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public string PreviousEmployer { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReferenceContact { get; set; }
    }
}
