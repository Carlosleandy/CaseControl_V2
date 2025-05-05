using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.DATA
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<CaseStatus> CaseStatuses { get; set; }
        public DbSet<CaseType> CaseTypes { get; set; }
        public DbSet<ReceptionMedium> ReceptionMedia { get; set; }
        public DbSet<RecommendationStatus> RecommendationStatuses { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<vwEmployee> vwEmployees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Binnacle> Binnacles { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<WorkingGroup> WorkingGroups { get; set; }
        public DbSet<RecommendationType> RecommendationTypes { get; set; }
        public DbSet<RecoveryHistory> RecoveryHistories { get; set; }
        public DbSet<CaseAssignment> CaseAssignments { get; set; }
        public DbSet<CaseStatusChange> CaseStatusChanges { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Access_Role> Access_Roles { get; set; }
        public DbSet<vwOficinas> VwOficinas { get; set; }
        public DbSet<vwCostCenter> VwCostCenters { get; set; }
        public DbSet<FaultType> FaultTypes { get; set; }
        public DbSet<IntervieweeType> IntervieweeTypes { get; set; }
        public DbSet<LinkType> LinkTypes { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Linked> Linkeds { get; set; }
        public DbSet<RelLinkedFault> RelLinkedFaults { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<EvidenceClassification> EvidenceClassifications { get; set; }
        public DbSet<Evidence> Evidences { get; set; }
        public DbSet<FaultLinked> FaultLinkeds { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CaseStatus>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<CaseType>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<ReceptionMedium>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<RecommendationStatus>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Recommendation>().HasIndex(x => x.Title).IsUnique();
            modelBuilder.Entity<RecommendationType>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<Binnacle>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<UserLevel>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<WorkingGroup>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Access>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<LinkType>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<EvidenceClassification>().HasIndex(x => x.Name).IsUnique();



            var userBinnacle = modelBuilder.Entity<User>();
            userBinnacle.HasMany<Binnacle>(p => p.Binnacles)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserID)
            .OnDelete(DeleteBehavior.Restrict);

            var userRecommendation = modelBuilder.Entity<User>();
            userRecommendation.HasMany<Recommendation>(p => p.Recommendations)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserID)
            .OnDelete(DeleteBehavior.Restrict);

            var userAmountRecovery = modelBuilder.Entity<User>();
            userAmountRecovery.HasMany<RecoveryHistory>(p => p.RecoveryHistories)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserID)
            .OnDelete(DeleteBehavior.Restrict);

            var caseassignment = modelBuilder.Entity<User>();
            caseassignment.HasMany<CaseAssignment>(p => p.CaseAssignments)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserID)
            .OnDelete(DeleteBehavior.Restrict);


            var casestatuschange = modelBuilder.Entity<Case>();
            casestatuschange.HasMany<CaseStatusChange>(p => p.CaseStatusChanges)
            .WithOne(t => t.Case)
            .HasForeignKey(t => t.CaseID)
            .OnDelete(DeleteBehavior.Restrict);

            var caseslinks = modelBuilder.Entity<Case>();
            caseslinks.HasMany<Linked>(p => p.Linkeds)
            .WithOne(t => t.Case)
            .HasForeignKey(t => t.CaseID)
            .OnDelete(DeleteBehavior.Restrict);


            var casestatus = modelBuilder.Entity<CaseStatus>();
            casestatus.HasMany<CaseStatusChange>(p => p.CaseStatusChanges)
            .WithOne(t => t.CaseStatus)
            .HasForeignKey(t => t.CaseStatusID)
            .OnDelete(DeleteBehavior.Restrict);


            var userLevel = modelBuilder.Entity<Role>();
            userLevel.HasMany<UserLevel>(p => p.UserLevels)
            .WithOne(t => t.Role)
            .HasForeignKey(t => t.RoleID)
            .OnDelete(DeleteBehavior.Restrict);

            var accessrole = modelBuilder.Entity<Role>();
            accessrole.HasMany<Access_Role>(p => p.Access_Roles)
            .WithOne(t => t.Role)
            .HasForeignKey(t => t.RoleID)
            .OnDelete(DeleteBehavior.Restrict);

            var access = modelBuilder.Entity<Access>();
            access.HasMany<Access_Role>(p => p.Access_Roles)
            .WithOne(t => t.Access)
            .HasForeignKey(t => t.AccessID)
            .OnDelete(DeleteBehavior.Restrict);



        }

    }
}
