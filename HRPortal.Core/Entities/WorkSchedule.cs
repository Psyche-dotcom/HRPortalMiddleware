namespace HRPortal.Core.Entities
{
    public class WorkSchedule : BaseEntity
    {
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public int HoursPerWeek { get; set; }
        public string Shift { get; set; }
    }
}

