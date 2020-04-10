//using MGA.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MGA.Infra.Data.EntityConfig
//{
//	public class UserConfig : EntityTypeConfiguration<User>
//	{
//		public UserConfig()
//		{
//			HasKey(c => c.ID);

//			Property(c => c.Domain)
//				.IsRequired()
//				.HasMaxLength(150);

//			Property(c => c.UserName)
//				.IsRequired()
//				.HasMaxLength(150);
//		}
//	}
//}
