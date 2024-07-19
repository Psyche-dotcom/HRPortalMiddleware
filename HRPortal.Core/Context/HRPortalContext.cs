using HRPortal.Core.Entities;
using HRPortal.Enitities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace HRPortal.Core.Context
{
    public class HRPortalContext : IdentityDbContext<ApplicationCompany>
    {
        public DbSet<ConfirmEmailToken> ConfirmEmailTokens { get; set; }
        public DbSet<ForgetPasswordToken> ForgetPasswordTokens { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<Compensation> Compensations { get; set; }
        public DbSet<BankingInfo> BankingInfos { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<TaxInformation> TaxInformations { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public DbSet<TrainingRecord> TrainingRecords { get; set; }
        public DbSet<CompanyProperty> CompanyProperties { get; set; }
        public DbSet<CompanySubscription> CompanySubscriptions { get; set; }
        public HRPortalContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
        }
       
    }

}
