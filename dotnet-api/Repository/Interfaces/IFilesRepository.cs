using dotnet_api.Models.Dtos;
using dotnet_api.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace dotnet_api.Repository.Interfaces
{
    public interface IFilesRepository : IBaseRepository
    {
        Task<IEnumerable<FilesDto>> GetFilesAsync();
        Task<FilesDto> GetFilesByIdAsync(int id);
    }
}
