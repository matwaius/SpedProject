﻿using dotnet_api.Context;
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
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        private readonly dotnet_apiContext _context;

        public UsersRepository(dotnet_apiContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UsersDto>> GetUsersAsync()
        {
            return await _context.Users
                .Select(x => new UsersDto { Id = x.Id, Login = x.Login })
                .ToListAsync();
        }

        public async Task<UsersDto> GetUsersByIdAsync(int Id)
        {
            return await _context.Users
                        .Select(x => new UsersDto { Id = x.Id, Login = x.Login })
                        .Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Users> GetUsersByLoginPasswordAsync(string Login, string Password)
        {
            return await _context.Users
                        .Where(x => x.Login == Login && x.Password == Password).FirstOrDefaultAsync(); ;
        }
    }
}