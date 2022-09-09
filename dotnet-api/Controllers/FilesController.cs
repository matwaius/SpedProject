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
using System.Text;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;

        public FilesController(IFilesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Post(IFormFile files)
        {
            try
            {
                bool invalido = false;
                if (files != null)
                {
                    if (files.Length > 0) //tamanhoArquivo
                    {
                        var result = new StringBuilder();
                        using (var reader = new StreamReader(files.OpenReadStream()))
                        {
                            while (reader.Peek() >= 0)
                                result.AppendLine(reader.ReadLine());
                        }

                        //Verifica Arquivo
                        if (!result.ToString().Contains("|"))
                        {
                            invalido = true;
                        }
                        else
                        {
                            SqlConnection conn = new SqlConnection();
                            conn.ConnectionString = "Data Source=localhost;Initial Catalog=Base;User Id=sa;password=projetounisul";
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;

                            cmd.CommandText = "DELETE FROM Files";
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            //Getting FileName
                            var fileName = Path.GetFileName(files.FileName);
                            //Getting file Extension
                            var fileExtension = Path.GetExtension(fileName);
                            // concatenating  FileName + FileExtension
                            var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                            var objfiles = new Files()
                            {
                                Id = 0,
                                Name = newFileName,
                                FileType = fileExtension,
                                CreatedOn = DateTime.Now
                            };

                            using (var target = new MemoryStream())
                            {
                                files.CopyTo(target);
                                objfiles.DataFiles = target.ToArray();
                            }

                            var fileInsert = _mapper.Map<Files>(objfiles);

                            _repository.Add(fileInsert);
                        }
                    }
                    else
                    {
                        invalido = true;
                    }
                }
                else
                {
                    invalido = true;
                }

                if (invalido == true)
                {
                    return BadRequest("Arquivo Inválido!");
                }
            }
            catch
            {
                return BadRequest("Erro ao Carregar Arquivo.");
            }
            return await _repository.SaveChangesAsync()
                            ? Ok("Arquivo Salvo com Sucesso.")
                            : BadRequest("Erro ao salvar o Arquivo.");
        }


        [HttpGet]
        private async Task<IActionResult> Get()
        {
            var files = await _repository.GetFilesAsync();

            return files.Any()
                    ? Ok(files)
                    : BadRequest("Arquivos não encontrados.");
        }

        [HttpGet("{id}")]
        private async Task<IActionResult> GetById(int id)
        {
            var file = await _repository.GetFilesByIdAsync(id);

            var fileRet = _mapper.Map<FilesDto>(file);

            return fileRet != null
                ? Ok(fileRet)
                : BadRequest("Arquivo não encontrado.");
        }

        [HttpDelete]
        private async Task<IActionResult> Delete()
        {
            var filesDelete = await _repository.GetFilesAsync();

            if (filesDelete == null) return NotFound("Arquivos não encontrado");

            _repository.Delete(filesDelete);

            return await _repository.SaveChangesAsync()
                 ? Ok("Arquivos deletados com Sucesso.")
                 : BadRequest("Erro ao deletar o Arquivos.");
        }

        [HttpDelete("{id}")]
        private async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Arquivo não encontrado.");

            var filesDelete = await _repository.GetFilesByIdAsync(id);

            if (filesDelete == null) return NotFound("Arquivo não encontrado");

            _repository.Delete(filesDelete);

            return await _repository.SaveChangesAsync()
                 ? Ok("Arquivo deletado com Sucesso.")
                 : BadRequest("Erro ao deletar o Arquivo.");
        }

    }
}
