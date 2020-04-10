//using MGA.Domain.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace MGA.Infra.Data.EntityConfig
//{
//    public class AssetStatusConfig 
//    {
//        public AssetStatusConfig(EntityTypeBuilder<AssetStatus> entityBuilder)
//        {
//            Property(c => c.Name)
//                .IsRequired()
//                .HasMaxLength(150);


//            entityBuilder.HasKey(t => t.Id);
//            entityBuilder.Property(t => t.FirstName).IsRequired();
//            entityBuilder.Property(t => t.LastName).IsRequired();
//            entityBuilder.Property(t => t.Email).IsRequired();
//        }
//    }
//}
