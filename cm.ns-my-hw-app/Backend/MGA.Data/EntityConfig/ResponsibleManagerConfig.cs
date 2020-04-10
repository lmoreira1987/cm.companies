//using MGA.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MGA.Infra.Data.EntityConfig
//{
//	public class ResponsibleManagerConfig : EntityTypeConfiguration<ResponsibleManager>
//	{
//		public ResponsibleManagerConfig()
//		{
//			Property(c => c.ADUsername)
//				.IsRequired()
//				.HasMaxLength(20);

//			Property(c => c.FirstName)
//				.IsRequired()
//				.HasMaxLength(60);

//			Property(c => c.LastName)
//				.IsRequired()
//				.HasMaxLength(60);

//			Property(c => c.Status)
//				.IsRequired();
//		}
//	}
//}
