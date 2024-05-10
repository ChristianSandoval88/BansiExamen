
namespace APIExamen.Data
{
    using APIExamen.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BdiExamenEntities : DbContext
    {
        public BdiExamenEntities()
            : base("name=BdiExamenEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblExamen> tblExamen { get; set; }
    }
}
