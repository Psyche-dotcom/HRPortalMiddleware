using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.Entities
{
    public class BankingInfo : BaseEntity
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string? RoutingNumber { get; set; }
        public Compensation Compensation { get; set; }
        public string CompensationId { get; set; }

    }
}
