//using MGA.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MGA.Infra.Data.EntityConfig
//{
//	public class HazardConfig : EntityTypeConfiguration<Hazard>
//	{
//		public HazardConfig()
//		{
//			Property(c => c.Code)
//				.IsRequired()
//				.HasMaxLength(1);

//			Property(c => c.Name)
//				.IsRequired()
//				.HasMaxLength(150);
//		}
//	}
//}
