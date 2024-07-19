using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.Entities
{
    public class Education : BaseEntity
    {
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public string InstitutionName { get; set; }
        public string Degree { get; set; }
        public DateTime GraduationDate { get; set; }
    }
}
