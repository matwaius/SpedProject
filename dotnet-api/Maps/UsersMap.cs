using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace dotnet_api.Maps
{
    public class UsersMap : BaseMap<Users>
    {
        public UsersMap() : base("Users")
        {

        }

        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Login).HasColumnName("Login").HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Password).HasColumnName("Password").HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar(255)").IsRequired();
            builder.Property(x => x.Nivel).HasColumnName("Nivel").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.RelNF).HasColumnName("RelNF").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.RelNFICMS).HasColumnName("RelNFICMS").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.RelNFICMSST).HasColumnName("RelNFICMSST").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.RelNFUF).HasColumnName("RelNFUF").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.RelNFQTD).HasColumnName("RelNFQTD").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.RelNFQTDUF).HasColumnName("RelNFQTDUF").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.RelCF).HasColumnName("RelCF").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.RelCFTOT).HasColumnName("RelCFTOT").HasColumnType("smallint").IsRequired();
        }
    }
}
