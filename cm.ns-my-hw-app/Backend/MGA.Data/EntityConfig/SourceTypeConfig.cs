//using MGA.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MGA.Infra.Data.EntityConfig
//{
//	public class SourceTypeConfig : EntityTypeConfiguration<SourceType>
//	{
//		public SourceTypeConfig()
//		{
//			Property(c => c.Code)
//				.IsRequired()
//				.HasMaxLength(3);

//			Property(c => c.Name)
//				.IsRequired()
//				.HasMaxLength(500);
//		}
//	}
//}
