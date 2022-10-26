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
    public class ReportTotalizersItemsDayController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportTotalizersItemsDayController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromQuery] DateTime date, [FromQuery] Int64 red)
        {
            string retRel = "";
            try
            {
                if (date == null)
                {
                    return BadRequest("Data Invalida!");
                }

                //Le o Arquivo do Banco
                string file = Library.DownFile(_configuration);
                if (file.Contains("|") == false || file.Trim().Length == 0)
                {
                    return BadRequest("Arquivo Invalido!");
                }

                List<C405> list = new List<C405>();

                C405 c405 = new C405();
                bool add = false;
                foreach (string line in file.Split('\n'))
                {
                    if (line != "")
                    {
                        string[] data = line.Split("|");

                        //C405 
                        if (data[1] == "C405")
                        {
                            add = false;
                            if (Library.GetDecimal(data[7]) > 0 && date == Library.ToDateTime(data[2], "ddMMyyyy"))
                            {
                                if (Library.GetInt64(data[4]) != red && red > 0) //Num. Redução Z
                                {
                                    //Não Adiciona
                                }
                                else
                                {
                                    c405 = new C405();
                                    c405.REG = Library.GetString(data[1]);
                                    c405.DT_DOC = Library.ToDateTime(data[2], "ddMMyyyy");
                                    c405.CRO = Library.GetInt32(data[3]);
                                    c405.CRZ = Library.GetInt32(data[4]);
                                    c405.NUM_COO_FIN = Library.GetInt32(data[5]);
                                    c405.GT_FIN = Library.GetDecimal(data[6]);
                                    c405.VL_BRT = Library.GetDecimal(data[7]);
                                    c405.Itens = new List<C420>();
                                    list.Add(c405);
                                    add = true;
                                }
                            }
                        }

                        //C420 
                        if (data[1] == "C420" && add == true)
                        {
                            C420 c420 = new C420();
                            c420.REG = Library.GetString(data[1]);
                            c420.COD_TOT_PAR = Library.GetString(data[2]);
                            c420.VLR_ACUM_TOT = Library.GetDecimal(data[3]);
                            c420.NR_TOT = Library.GetInt32(data[4]);
                            c420.DESCR_NR_TOT = Library.GetString(data[5]);
                            c405.Itens.Add(c420);
                        }
                    }
                }

                if (list.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                {
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
