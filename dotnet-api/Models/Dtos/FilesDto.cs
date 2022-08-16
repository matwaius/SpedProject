namespace dotnet_api.Models.Dtos
{
    public class FilesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
