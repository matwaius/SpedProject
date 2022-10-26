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
    public class ReportCFDayController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportCFDayController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd, [FromQuery] Int64 doc, [FromQuery] Int64 red)
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

                List<C460> list = new List<C460>();
                C460 c460 = new C460();
                bool add = true;
                foreach (string line in file.Split('\n'))
                {
                    if (line != "")
                    {
                        string[] data = line.Split("|");
                        
                        //C405 
                        if (data[1] == "C405" && red > 0)
                        {
                            add = false;
                            if (Library.GetInt64(data[4]) == red) //Num. Redução Z
                            {
                                add = true;
                            }
                        }

                        //C460 
                        if (data[1] == "C460" && add == true)
                        {
                            if (doc > 0 && Library.GetInt64(data[4]) != doc)
                            {
                                //Não Adiciona
                            }
                            else
                            {
                                list.Add(c460.MountDataC460(data));
                            }
                        }
                    }
                }

                if (list.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                {
                    DataTable data = Library.ToDataTable(list);

                    var dateFormat = data
                                        .Select()
                                        .Where(x => Library.GetDateTime(x["DT_DOC"].ToString()) >= dateStart && Library.GetDateTime(x["DT_DOC"].ToString()) <= dateEnd) 
                                        .GroupBy(g => new
                                        {
                                            grp_date = g["DT_DOC"]
                                        })
                                        .Select(s => new
                                        {
                                            DT_DOC = s.Key.grp_date.ToString().Substring(0,10),
                                            VL_DOC = s.Sum(ss => Library.GetDecimal(ss["VL_DOC"].ToString()))
                                        }).ToList().OrderBy(o => o.DT_DOC);

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
