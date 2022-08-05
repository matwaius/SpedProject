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
        }
    }
}
