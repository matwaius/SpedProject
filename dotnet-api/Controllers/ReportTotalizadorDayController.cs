using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using dotnet_api.Repository.Interfaces;
using AutoMapper;
using System.Text;
using SqlConnection = System.Data.SqlClient.SqlConnection;
using SqlCommand = System.Data.SqlClient.SqlCommand;
using Microsoft.Extensions.Configuration;
using dotnet_api.Models.Dtos;
using Newtonsoft.Json;
using dotnet_api.Methods.Classes;
using dotnet_api.Methods;
using System.Globalization;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportTotalizadorDayController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportTotalizadorDayController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("{dateStart}&{dateEnd}")]
        public async Task<IActionResult> Post(DateTime dateStart, DateTime dateEnd)
        {
            string retRel = "";
            try
            {
                if (dateEnd < dateStart || dateStart > dateEnd)
                {
                    return BadRequest("Data Invalida!");
                }

                //Le o Arquivo do Banco
                string file = Library.DownFile(_configuration);
                if (file.Contains("|") == false || file.Trim().Length == 0)
                {
                    return BadRequest("Arquivo Invalido!");
                }

                List<object> list = new List<object>();

                foreach (string line in file.Split('\n'))
                {
                    if (line != "")
                    {
                        string[] data = line.Split("|");

                        //C405 
                        if (data[1] == "C405")
                        {
                            C405 c405 = new C405();
                            list.Add(c405.MountDataC405(data));
                        }

                        //C420 
                        if (data[1] == "C420")
                        {
                            C420 c420 = new C420();
                            list.Add(c420.MountDataC420(data));
                        }
                    }
                }

                if (list.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                {
                    /*var x = (from item in list
                     select new { item., item.Name }).ToList();
                    var xx = list.Select(x => new C405() { REG }).ToList();*/

                    retRel = JsonConvert.SerializeObject(list);
                }
            }
            catch
            {
                return BadRequest("Erro ao Executar Relatório.");
            }

            return retRel != "[]" && retRel != null && retRel != ""
                    ? Ok(retRel)
                    : BadRequest("Nenhum Registro Encontrado.");
        }
      
    }
}
