using MedMonitor24_7.Models;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;

namespace MedMonitor24_7.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Unit> Units { get; set; } = default!;
        public DbSet<Bed> Beds { get; set; } = default!;
        public DbSet<Patient> Patients { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<SystemUser> SystemUsers { get; set; } = default!;
        public DbSet<UserUnit> UserUnits { get; set; } = default!;
        public DbSet<Admission> Admissions { get; set; } = default!;
        public DbSet<AdmissionRequest> AdmissionRequests { get; set; } = default!;

        public DbSet<VitalSignType> VitalSignTypes { get; set; } = default!;
        public DbSet<VitalSignReading> VitalSignReadings { get; set; } = default!;

        public DbSet<ReportCategory> ReportCategories { get; set; } = default!;
        public DbSet<PatientReport> PatientReports { get; set; } = default!;
        public DbSet<ReportFile> ReportFiles { get; set; } = default!;

        public DbSet<ClinicalTask> ClinicalTasks { get; set; } = default!;
        public DbSet<TaskEvent> TaskEvents { get; set; } = default!;

        public DbSet<NotificationSettings> NotificationSettings { get; set; } = default!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = default!;

        public DbSet<AdmissionVitalThreshold> AdmissionVitalThresholds { get; set; } = default!;
        public DbSet<Alert> Alerts { get; set; } = default!;
        public DbSet<AlertAcknowledgement> AlertAcknowledgements { get; set; } = default!;

        public DbSet<Device> Devices { get; set; } = default!;
        public DbSet<DeviceHeartbeat> DeviceHeartbeats { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-HSF8481\\SQLEXPRESS;" +
                    "Database=MedMonitor24_7;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // =========================
            // Composite Key
            // =========================
            modelBuilder.Entity<UserUnit>()
                .HasKey(uu => new { uu.UserID, uu.UnitID });

            // =========================
            // Unit العلاقات
            // =========================
            modelBuilder.Entity<Unit>()
                .HasMany(u => u.Beds)
                .WithOne(b => b.Unit)
                .HasForeignKey(b => b.UnitID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserUnit>()
                .HasOne(uu => uu.User)
                .WithMany(u => u.UserUnits)
                .HasForeignKey(uu => uu.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserUnit>()
                .HasOne(uu => uu.Unit)
                .WithMany(u => u.UserUnits)
                .HasForeignKey(uu => uu.UnitID)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // Role / Users
            // =========================
            modelBuilder.Entity<SystemUser>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SystemUser>()
                .HasOne(u => u.NotificationSettings)
                .WithOne(s => s.User)
                .HasForeignKey<NotificationSettings>(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // Admissions
            // =========================
            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Admissions)
                .HasForeignKey(a => a.PatientID)
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

            // active admission per bed
            modelBuilder.Entity<Admission>()
                .HasIndex(a => a.BedID)
                .IsUnique()
                .HasFilter("[DischargeDate] IS NULL");

            // active admission per patient
            modelBuilder.Entity<Admission>()
                .HasIndex(a => a.PatientID)
                .IsUnique()
                .HasFilter("[DischargeDate] IS NULL");

            // =========================
            // Admission Requests
            // =========================
            modelBuilder.Entity<AdmissionRequest>()
                .HasOne(ar => ar.RequestedByDoctor)
                .WithMany(u => u.RequestedAdmissionRequests)
                .HasForeignKey(ar => ar.RequestedByDoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdmissionRequest>()
                .HasOne(ar => ar.ReviewedByAdmin)
                .WithMany(u => u.ReviewedAdmissionRequests)
                .HasForeignKey(ar => ar.ReviewedByAdminID)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Vital Signs
            // =========================
            modelBuilder.Entity<VitalSignReading>()
                .HasOne(v => v.Admission)
                .WithMany(a => a.VitalSignReadings)
                .HasForeignKey(v => v.AdmissionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VitalSignReading>()
                .HasOne(v => v.VitalSignType)
                .WithMany(vt => vt.VitalSignReadings)
                .HasForeignKey(v => v.VitalSignTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VitalSignReading>()
                .HasIndex(v => new { v.AdmissionID, v.VitalSignTypeID, v.ReadingTime });

            // =========================
            // Patient Reports
            // =========================
            modelBuilder.Entity<PatientReport>()
                .HasOne(pr => pr.Admission)
                .WithMany(a => a.PatientReports)
                .HasForeignKey(pr => pr.AdmissionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientReport>()
                .HasOne(pr => pr.Category)
                .WithMany(c => c.PatientReports)
                .HasForeignKey(pr => pr.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientReport>()
                .HasOne(pr => pr.UploadedByUser)
                .WithMany(u => u.UploadedReports)
                .HasForeignKey(pr => pr.UploadedByUserID)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Clinical Tasks
            // =========================
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

            modelBuilder.Entity<ClinicalTask>()
                .HasIndex(t => new { t.AssignedNurseID, t.Status, t.StartDateTime });

            // =========================
            // Chat
            // =========================
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

            // =========================
            // Indexes
            // =========================
            modelBuilder.Entity<Bed>()
                .HasIndex(b => new { b.UnitID, b.BedCode })
                .IsUnique();

            modelBuilder.Entity<SystemUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Unit>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<VitalSignType>()
                .HasIndex(v => v.Name)
                .IsUnique();

            modelBuilder.Entity<ReportCategory>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Alert>()
                .HasIndex(a => new { a.AdmissionID, a.Status, a.RaisedAt });

            // =========================
            // DateTime => datetime2
            // =========================
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("datetime2");
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}