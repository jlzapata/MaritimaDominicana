namespace MaritimaDominicana.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SupportContext : DbContext
    {
        public SupportContext()
            : base("name=SupportContext")
        {
        }

        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Problem> Problems { get; set; }
        public virtual DbSet<Models.Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ProblemDetail> ProblemDetails { get; set; }
        public virtual DbSet<Solution> Solutions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProblemDetail>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<ProblemDetail>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProblemDetail>()
                .Property(e => e.Update_at)
                .IsOptional();

            modelBuilder.Entity<Place>()
                .HasMany(e => e.ProblemDetails)
                .WithRequired(e => e.Place)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Problem>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Problem>()
                .HasMany(e => e.ProblemDetails)
                .WithRequired(e => e.Problem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Pasword)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProblemDetails)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Solutions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Followeds)
                .WithMany(e => e.Followers)
                .Map(m => m.ToTable("Followers").MapLeftKey("UserId").MapRightKey("FollowerId"));

            modelBuilder.Entity<Solution>()
                .Property(e => e.SolutionDescription)
                .IsUnicode(false);
        }
    }
}
