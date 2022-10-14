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
    public class ReportNFDepartureDayByRedZController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportNFDepartureDayByRedZController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd)
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

                List<C405> listC405 = new List<C405>();
                foreach (string line in file.Split('\n'))
                {
                    if (line != "")
                    {
                        string[] data = line.Split("|");

                        //C405 
                        if (data[1] == "C405")
                        {
                            C405 c405 = new C405();
                            listC405.Add(c405.MountDataC405(data));
                        }
                    }
                }

                if (listC405.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                {
                    DataTable data = Library.ToDataTable(listC405);

                    var dateFormat = data
                                        .Select()
                                        .Where(x => Library.GetDateTime(x["DT_DOC"].ToString()) >= dateStart && Library.GetDateTime(x["DT_DOC"].ToString()) <= dateEnd) 
                                        .GroupBy(g => new
                                        {
                                            grp_date = g["DT_DOC"].ToString().Substring(0,10)
                                        })
                                        .Select(s => new
                                        {
                                            DT_DOC = s.Key.grp_date,
                                            VL_BRT = s.Sum(ss => Library.GetDecimal(ss["VL_BRT"].ToString()))
                                        }).ToList().OrderBy(o=>o.DT_DOC);

                    retRel = JsonConvert.SerializeObject(dateFormat);
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
