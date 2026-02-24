using MedMonitor24_7.Models;
using Microsoft.EntityFrameworkCore;

namespace MedMonitor24_7.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Unit> Units { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<UserUnit> UserUnits { get; set; }
        public DbSet<Admission> Admissions { get; set; }

        public DbSet<VitalSignType> VitalSignTypes { get; set; }
        public DbSet<VitalSignReading> VitalSignReadings { get; set; }

        public DbSet<ReportCategory> ReportCategories { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }
        public DbSet<ReportFile> ReportFiles { get; set; }

        public DbSet<ClinicalTask> ClinicalTasks { get; set; }
        public DbSet<TaskEvent> TaskEvents { get; set; }

        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<AdmissionVitalThreshold> AdmissionVitalThresholds { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<AlertAcknowledgement> AlertAcknowledgements { get; set; }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceHeartbeat> DeviceHeartbeats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-HSF8481\\SQLEXPRESS;" +
                    "Database=MedMonitor24_7;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=True;"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<UserUnit>()
                .HasKey(x => new { x.UserID, x.UnitID });

            
            modelBuilder.Entity<SystemUser>()
                .HasOne(u => u.NotificationSettings)
                .WithOne(s => s.User)
                .HasForeignKey<NotificationSettings>(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Admissions)
                .HasForeignKey(a => a.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Bed)
                .WithMany(b => b.Admissions)
                .HasForeignKey(a => a.BedID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Doctor)
                .WithMany(u => u.DoctorAdmissions)
                .HasForeignKey(a => a.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Nurse)
                .WithMany(u => u.NurseAdmissions)
                .HasForeignKey(a => a.NurseID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasIndex(a => a.BedID)
                .IsUnique()
                .HasFilter("[DischargeDate] IS NULL");

            modelBuilder.Entity<Admission>()
                .HasIndex(a => a.PatientID)
                .IsUnique()
                .HasFilter("[DischargeDate] IS NULL");

            
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.SenderUser)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.ReceiverUser)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverUserID)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Bed>()
                .HasIndex(b => new { b.UnitID, b.BedCode })
                .IsUnique();

            modelBuilder.Entity<SystemUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<VitalSignReading>()
                .HasIndex(v => new { v.AdmissionID, v.VitalSignTypeID, v.ReadingTime });

            modelBuilder.Entity<Alert>()
                .HasIndex(a => new { a.AdmissionID, a.Status, a.RaisedAt });

            modelBuilder.Entity<ClinicalTask>()
                .HasIndex(t => new { t.AssignedNurseID, t.Status, t.StartDateTime });
            
            modelBuilder.Entity<ClinicalTask>()
                .HasOne(t => t.Admission)
                .WithMany(a => a.ClinicalTasks)
                .HasForeignKey(t => t.AdmissionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClinicalTask>()
                .HasOne(t => t.CreatedByUser)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatedByUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClinicalTask>()
                .HasOne(t => t.AssignedNurse)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedNurseID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}