using PRS.Migrations;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PRS.Models
{
    public class PRSContext : DbContext
    {
        public string UserName { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Jail> Jails { get; set; }
        public DbSet<CrimeType> CrimeTypes { get; set; }
        public DbSet<Act> Acts { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<MedicalTest> MedicalTests { get; set; }
        public DbSet<PoliceStation> PoliceStations { get; set; }
        public DbSet<PrisonerType> PrisonerTypes { get; set; }
        public DbSet<PrisonerSubType> PrisonerSubTypes { get; set; }
        public DbSet<CourtType> CourtTypes { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<JudgeType> JudgeTypes { get; set; }
        public DbSet<RelationType> RelationTypes { get; set; }
        public DbSet<Judge> Judges { get; set; }
        
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Settings> Setttings { get; set; }

        public DbSet<Prisoner> Prisoners { get; set; }
        public DbSet<Admission> Admissions { get; set; }

        public DbSet<RemissionType> RemissionTypes { get; set; }
        public DbSet<RemissionOrder> RemissionOrders { get; set; }
        public DbSet<EarnedRemission> EarnedRemissions { get; set; }

        public DbSet<CourtHearing> CourtHearings { get; set; }
        public DbSet<CourtDecision> CourtDecisions { get; set; }
        public DbSet<CourtDecisionSection> CourtDecisionSections { get; set; }
        public DbSet<Appeal> Appeals { get; set; }

        public DbSet<FIR> FIRs { get; set; }
        public DbSet<MedicalTreatment> MedicalTreatments { get; set; }
        public DbSet<MedicalCheckup> MedicalCheckups { get; set; }
        public DbSet<PrescribedTest> PrescribedTests { get; set; }
        public DbSet<Child> Children { get; set; }

        public DbSet<PrisonOffence> PrisonOffences { get; set; }        
        public DbSet<PrisonerTransaction> PrisonerTransactions { get; set; }
        public DbSet<PrisonerProperty> PrisonerProperties { get; set; }        

        public DbSet<CheckInOut> CheckInOuts { get; set; }

        public DbSet<Barrack> Barracks { get; set; }
        public DbSet<MedicalOfficer> MedicalOfficers { get; set; }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Visit> Visits { get; set; }

        //public DbSet<Prison> Prisons { get; set; }
        //public DbSet<MedicalExam> MedicalExams { get; set; }
        //public DbSet<Classification> Classifications { get; set; }
        //public DbSet<PrisonerAdmission> PrisonerAdmissions { get; set; }

        public PRSContext()
            : base("DefaultConnection")
        {
            UserName = "SYSTEM";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        private long GetPrimaryKeyValue(object entity)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            return Convert.ToInt64(objectStateEntry.EntityKey.EntityKeyValues[0].Value);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().GetEnumerator();
            entries.MoveNext();

            var entry = entries.Current;

            if (entry == null)
                return 0;

            AuditLog log = new AuditLog();

            log.Operation = entry.State.ToString().Substring(0, 1);
            log.Entity = entry.Entity.ToString().Replace("PRS.Models.", "");

            if (entry.State != EntityState.Added)
                log.EntityKey = GetPrimaryKeyValue(entry.Entity);

            log.UserName = UserName;

            log.OperationDate = DateTime.Now;

            AuditLogs.Add(log);

            int count = base.SaveChanges();

            if (log.EntityKey == 0)
            {
                log.EntityKey = GetPrimaryKeyValue(entry.Entity);
                Entry(log).State = EntityState.Modified;
                base.SaveChanges();
            }

            return count;
        }
    }

    internal class PRSInitializer : MigrateDatabaseToLatestVersion<PRSContext, Configuration>
    {
        //protected override void Seed(PRSContext context)
        //{
        //    base.Seed(context);

        //    context.Prisons.AddOrUpdate(new Prison { Capacity = 5000, Name = "Default Prison", District = "Default District", Province = "Default Province" });

        //    context.Classifications.AddOrUpdate(new Classification { Name = "Non Serious" });
        //    context.Classifications.AddOrUpdate(new Classification { Name = "Serious" });

        //    context.SaveChanges();
        //}
    }
}