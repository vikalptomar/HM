using Microsoft.EntityFrameworkCore;
using HealthcareManagementSystem.Models;

namespace HealthcareManagementSystem.DB
{
    public class HM_dbContext : DbContext
    {
        public HM_dbContext(DbContextOptions<HM_dbContext> options) : base(options) { }

        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<UsersModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientModel>(entity =>
            {
                entity.ToTable("patient");
                entity.HasKey(e => e.PatientId).HasName("PRIMARY");
                entity.Property(e => e.PatientId).HasColumnName("patient_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.DOB).HasColumnName("dob");
                entity.Property(e => e.Gender).HasColumnName("gender");
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.DateRegistered).HasColumnName("date_registered");
            }
            );
            modelBuilder.Entity<UsersModel>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.UserId).HasName("PRIMARY");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.UserName).HasColumnName("user_name");
                entity.Property(e => e.UserEmail).HasColumnName("user_email");
                entity.Property(e => e.UserPassword).HasColumnName("user_password");
            });
        }
    }
}
