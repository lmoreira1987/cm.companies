using MGA.CrossCutting.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MGA.Data.Context
{
    public class MGAContext : DbContext
	{
        public MGAContext(DbContextOptions<MGAContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
			//ConfigAsset(modelBuilder);

            base.OnModelCreating(modelBuilder);
		}

		//private void ConfigAsset(ModelBuilder modelBuilder)
		//{
		//	foreach (var property in modelBuilder.Model.GetEntityTypes()
		//	.SelectMany(t => t.GetProperties())
		//	.Where(p => p.Name == "DesignedMaxFlow" || 
		//				p.Name == "AvailableFlow" || 
		//				p.Name == "DesignedMaxStorage" || 
		//				p.Name == "AvailableStorage" || 
		//				p.Name == "AverageDailyFlow"))
		//	{
		//		property.Relational().ColumnType = "decimal(18, 3)";
		//	}
		//}
	}
}
