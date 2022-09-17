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
using AutoMapper;
using dotnet_api.Repository.Interfaces;
using dotnet_api.Models.Dtos;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _repository.GetUsersAsync();

            return users.Any()
                    ? Ok(users)
                    : BadRequest("Usuário não encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repository.GetUsersByIdAsync(id);

            var userRet = _mapper.Map<Users>(user);

            return userRet != null
                ? Ok(userRet)
                : BadRequest("Usuário não encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Users user)
        {
            if (user == null) return BadRequest("Dados Inválidos");

            var userInsert= _mapper.Map<Users>(user);

            _repository.Add(userInsert);

            return await _repository.SaveChangesAsync()
                ? Ok("Usuário adicionado com Sucesso.")
                : BadRequest("Erro ao salvar o Usuário.");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Users user)
        {
            if (id <= 0) return BadRequest("Usuário não informado");

            var userBanco = await _repository.GetUsersByIdAsync(id);

            var userUpdate = _mapper.Map(user, userBanco);

            _repository.Update(userUpdate);

            return await _repository.SaveChangesAsync()
                 ? Ok("Usuário atualizado com Sucesso.")
                 : BadRequest("Erro ao atualizar o Usuário.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Usuário inválido");

            var userDelete = await _repository.GetUsersByIdAsync(id);

            if (userDelete == null) return NotFound("Usuário não encontrado");

            _repository.Delete(userDelete);

            return await _repository.SaveChangesAsync()
                 ? Ok("Usuário deletado com Sucesso.")
                 : BadRequest("Erro ao deletar o Usuário.");
        }
    }
}