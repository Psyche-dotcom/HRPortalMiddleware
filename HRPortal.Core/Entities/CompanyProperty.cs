using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.Entities
{
    public class CompanyProperty : BaseEntity
    {
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public string PropertyName { get; set; }
        public string SerialNumber { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
