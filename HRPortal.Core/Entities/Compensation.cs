using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.Entities
{
    public class Compensation : BaseEntity
    {
        public decimal BaseSalary { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal Bonus { get; set; }
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public string PayFrequency { get; set; }
        public BankingInfo BankingInformation { get; set; }
    }
}
