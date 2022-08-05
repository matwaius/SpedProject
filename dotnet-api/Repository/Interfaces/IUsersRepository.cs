using dotnet_api.Models.Dtos;
using dotnet_api.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace dotnet_api.Repository.Interfaces
{
    public interface IUsersRepository : IBaseRepository
    {
        Task<IEnumerable<UsersDto>> GetUsersAsync();
        Task<Users> GetUsersByIdAsync(int id);
    }
}
