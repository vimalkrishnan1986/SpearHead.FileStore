using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using SpearHead.FileStore.Data.Entities;
namespace SpearHead.FileStore.Data
{
    public sealed class Context : DbContext
    {
        public DbSet<FileMetaData> FileMetaDatas { get; set; }

        public Context(string connectionString) : base(connectionString)
        {
            IDatabaseInitializer<Context> strategy = new CreateDatabaseIfNotExists<Context>();
            Database.SetInitializer<Context>(strategy);
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileMetaData>().ToTable("FileMetaData");
            modelBuilder.Entity<FileMetaData>().HasKey(p => p.Id);
            modelBuilder.Entity<FileMetaData>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            base.OnModelCreating(modelBuilder);
        }
    }
}
