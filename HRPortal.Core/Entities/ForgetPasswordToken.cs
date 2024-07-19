using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.Entities
{
    public class ForgetPasswordToken : BaseEntity
    {
        public string token { get; set; }
        public string gentoken { get; set; }
        public ApplicationCompany company { get; set; }
        public string companyid { get; set; }
    }
}
