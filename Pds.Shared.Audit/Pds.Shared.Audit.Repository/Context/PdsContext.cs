using Microsoft.EntityFrameworkCore;
using DataModels = Pds.Shared.Audit.Repository.DataModels;

#nullable disable

namespace Pds.Shared.Audit.Repository.Context
{
    /// <summary>
    /// PDS database context - Owner is monolith, this is created by reverse engineering existing database.
    /// Migrations should not be performed.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class PdsContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdsContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PdsContext(DbContextOptions<PdsContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdsContext"/> class.
        /// </summary>
        public PdsContext() : base()
        {
        }

        /// <summary>
        /// Gets or sets the audits DBSet.
        /// </summary>
        /// <value>
        /// The audits.
        /// </value>
        public virtual DbSet<DataModels.Audit> Audits { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<DataModels.Audit>(entity =>
            {
                entity.ToTable("Audits", "Contracts");
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.User)
                    .IsRequired()
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}