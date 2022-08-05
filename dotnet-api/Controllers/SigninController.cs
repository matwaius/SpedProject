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
        /*[HttpPost]
        public async Task<IActionResult> Post(SigninDto user)
        {
            if (user == null) return BadRequest("Dados Inválidos");

            var userInsert = _mapper.Map<User>(user);

            _repository.Add(userInsert);

            return await _repository.SaveChangesAsync()
                ? Ok("Usuário adicionado com Sucesso.")
                : BadRequest("Erro ao salvar o Usuário.");

        }*/
        #endregion

    }
}
