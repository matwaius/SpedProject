using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using dotnet_api.Models.Entities;
using dotnet_api.Repository.Interfaces;
using AutoMapper;
using dotnet_api.Models.Dtos;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;

        public SigninController(IUsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        #region POST - Signin 
        [HttpPost("{Login},{Password}")]
        public async Task<IActionResult> Post(string Login, string Password)
        {
            if (Login == null || Login == "") return BadRequest("Login Inválido");

            if (Password == null || Password == "") return BadRequest("Senha Inválida");

            var user = await _repository.GetUsersByLoginPasswordAsync(Login, Password);

            var ok = "Usuário Encontrado";

            return user != null
                ? Ok(ok)
                : BadRequest("Usuário não encontrado.");

        }
        #endregion

    }
}
