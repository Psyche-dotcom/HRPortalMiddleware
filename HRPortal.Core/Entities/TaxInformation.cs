using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.Entities
{
    public class TaxInformation : BaseEntity
    {
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public string W4FormDetails { get; set; }
        public string StateTaxWithholding { get; set; }
        public string LocalTaxWithholding { get; set; }
    }
}
