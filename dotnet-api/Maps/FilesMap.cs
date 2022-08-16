using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace dotnet_api.Maps
{
    public class FilesMap : BaseMap<Files>
    {
        public FilesMap() : base("Files")
        {

        }

        public override void Configure(EntityTypeBuilder<Files> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.FileType).HasColumnName("FileType").IsRequired();
            builder.Property(x => x.DataFiles).HasColumnName("DataFiles").IsRequired();
            builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn").IsRequired();
        }

    }
}
