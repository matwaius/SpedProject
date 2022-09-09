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

        public async Task<IEnumerable<Models.Entities.Files>> GetFilesAsync()
        {
            return await _context.Files
                .Select(x => new Models.Entities.Files { Id = x.Id, Name = x.Name, FileType = x.FileType, DataFiles = x.DataFiles, CreatedOn = x.CreatedOn })
                .ToListAsync();
        }

        public async Task<Models.Entities.Files> GetFileAsync()
        {
            return await _context.Files.FirstOrDefaultAsync();
        }

        

        public async Task<Models.Entities.Files> GetFilesByIdAsync(int Id)
        {
            return await _context.Files
                        .Select(x => new Models.Entities.Files { Id = x.Id, Name = x.Name, FileType = x.FileType, DataFiles = x.DataFiles, CreatedOn = x.CreatedOn })
                        .Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
    }
}
