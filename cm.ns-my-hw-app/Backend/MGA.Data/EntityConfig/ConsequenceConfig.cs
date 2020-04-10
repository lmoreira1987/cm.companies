//using MGA.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MGA.Infra.Data.EntityConfig
//{
//	public class ConsequenceConfig : EntityTypeConfiguration<Consequence>
//	{
//		public ConsequenceConfig()
//		{
//			HasRequired(e => e.Parameter)
//				.WithMany(c => c.Consequences)
//				.HasForeignKey(c => c.ParameterID);

//			HasRequired(e => e.Stage)
//			.WithMany(c => c.Consequences)
//			.HasForeignKey(c => c.StageID);
//		}
//	}
//}
