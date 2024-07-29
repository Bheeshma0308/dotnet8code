using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ExcellenceQuest.Repository.Models
{
    public partial class ExcellenceQuestContext : DbContext
    {
        public ExcellenceQuestContext()
        {
        }

        public ExcellenceQuestContext(DbContextOptions<ExcellenceQuestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<BadgeConfiguration> BadgeConfigurations { get; set; }
        public virtual DbSet<Competency> Competencies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeAchievement> EmployeeAchievements { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<KPISuccessCriterion> KPISuccessCriteria { get; set; }
        public virtual DbSet<KeyPerformanceIndex> KeyPerformanceIndices { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Stream> Streams { get; set; }
        public virtual DbSet<SubCompetency> SubCompetencies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ExcellenceQuest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Badge>(entity =>
            {
                entity.ToTable("Badge", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BadgeConfiguration>(entity =>
            {
                entity.ToTable("BadgeConfiguration", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.BadgeConfigurations)
                    .HasForeignKey(d => d.BadgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BadgeConf__Badge__619B8048");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.BadgeConfigurations)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BadgeConf__Grade__60A75C0F");

                entity.HasOne(d => d.Kpi)
                    .WithMany(p => p.BadgeConfigurations)
                    .HasForeignKey(d => d.KpiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BadgeConf__KpiId__5FB337D6");

                entity.HasOne(d => d.SubCompetency)
                    .WithMany(p => p.BadgeConfigurations)
                    .HasForeignKey(d => d.SubCompetencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BadgeConf__SubCo__5EBF139D");
            });

            modelBuilder.Entity<Competency>(entity =>
            {
                entity.ToTable("Competency", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "eq");

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Competency)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompetencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Competency");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Grade");

                entity.HasOne(d => d.SubCompetency)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.SubCompetencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_SubCompetency");
            });

            modelBuilder.Entity<EmployeeAchievement>(entity =>
            {
                entity.ToTable("EmployeeAchievements", "eq");

                entity.Property(e => e.AchievedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeAchievements)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeAchievements_Employee");

                entity.HasOne(d => d.Kpi)
                    .WithMany(p => p.EmployeeAchievements)
                    .HasForeignKey(d => d.KpiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EmployeeA__KpiId__52593CB8");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<KPISuccessCriterion>(entity =>
            {
                entity.ToTable("KPISuccessCriteria", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.KPISuccessCriteria)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KPISuccessCriteria_Grade");

                entity.HasOne(d => d.Kpi)
                    .WithMany(p => p.KPISuccessCriteria)
                    .HasForeignKey(d => d.KpiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KPISuccessCriteria_KeyPerformanceIndex");

                entity.HasOne(d => d.SubCompetency)
                    .WithMany(p => p.KPISuccessCriteria)
                    .HasForeignKey(d => d.SubCompetencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KPISuccessCriteria_SubCompetency");
            });

            modelBuilder.Entity<KeyPerformanceIndex>(entity =>
            {
                entity.ToTable("KeyPerformanceIndex", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Stream)
                    .WithMany(p => p.KeyPerformanceIndices)
                    .HasForeignKey(d => d.StreamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeyPerformanceIndex_Stream");

                entity.HasOne(d => d.SubCompetency)
                    .WithMany(p => p.KeyPerformanceIndices)
                    .HasForeignKey(d => d.SubCompetencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeyPerformanceIndex_SubCompetency");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Stream>(entity =>
            {
                entity.ToTable("Stream", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SubCompetency>(entity =>
            {
                entity.ToTable("SubCompetency", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Competency)
                    .WithMany(p => p.SubCompetencies)
                    .HasForeignKey(d => d.CompetencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubCompet__Compe__46E78A0C");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "eq");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole", "eq");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Employee");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__RoleId__5812160E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
