using dotnet_api.Context;
using dotnet_api.Models.Dtos;
using dotnet_api.Models.Entities;
using dotnet_api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace dotnet_api.Repository
{
    public class FilesRepository : BaseRepository, IFilesRepository
    {
        private readonly dotnet_apiContext _context;

        public FilesRepository(dotnet_apiContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Files>> GetFilesAsync()
        {
            return await _context.Files
                .ToListAsync();
        }

        public async Task<Files> GetFilesByIdAsync(int Id)
        {
            return await _context.Files
                        .Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
    }
}
