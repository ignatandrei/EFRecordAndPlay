using System.Data.Entity;

namespace EFRecordAndPlay_Use_MakingDemos.DatabaseRelated
{
    public partial class ContextForDatabase : DbContext
    {
        public ContextForDatabase()
            : base("name=Model1")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            
        }
        /// <summary>
        /// can be faked - but it is todays purpose
        /// </summary>
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Department>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.IDDepartment)
                .WillCascadeOnDelete(false);
        }
    }
}
