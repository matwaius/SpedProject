using AutoMapper;
using dotnet_api.Models.Dtos;
using dotnet_api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace dotnet_api.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Users, UsersDto> ();
            CreateMap<UsersInsertDto, Users>();
            CreateMap<UsersUpdateDto, Users>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

    }
}
