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
            bool invalido = false;

            //file lines
            //var file2 = System.IO.File.ReadAllText(@"C:\Users\zzzz.tmp");

            if (files != null)
            {
                if (files.Length > 0) //tamanhoArquivo
                {
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
                else
                {
                    invalido = true;
                }
            }
            else
            {
                invalido = true;
            }

            if(invalido == true)
            {
                return BadRequest("Dados Inválidos");
            }

            return await _repository.SaveChangesAsync()
                            ? Ok("Arquivo Salvo com Sucesso.")
                            : BadRequest("Erro ao salvar o Arquivo.");
        }


        /*[HttpGet]
        public async Task<IActionResult> Get()
        {
            var files = await _repository.GetFilesAsync();

            return files.Any()
                    ? Ok(files)
                    : BadRequest("Arquivo não encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var file = await _repository.GetFilesByIdAsync(id);

            var fileRet = _mapper.Map<FilesDto>(file);

            return fileRet != null
                ? Ok(fileRet)
                : BadRequest("Arquivo não encontrado.");
        }*/

    }
}
