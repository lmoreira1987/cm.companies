//using MGA.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MGA.Infra.Data.EntityConfig
//{
//    public class ParameterConfig : EntityTypeConfiguration<Parameter>
//    {
//        public ParameterConfig()
//        {
//            Property(c => c.Code)
//                .IsRequired()
//                .HasMaxLength(150);

//            Property(c => c.Name)
//                .IsRequired()
//                .HasMaxLength(150);

//            HasRequired(e => e.ParameterType)
//            .WithMany(c => c.Parameters)
//            .HasForeignKey(c => c.ParameterTypeID);

//            HasRequired(e => e.UnitOfMeasure)
//            .WithMany(c => c.Parameters)
//            .HasForeignKey(c => c.UnitOfMeasureID);
//        }
//    }
//}
