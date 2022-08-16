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
        }
    }
}
