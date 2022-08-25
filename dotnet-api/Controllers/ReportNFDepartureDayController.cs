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
using dotnet_api.Metodos.Classes;
using dotnet_api.Metodos;
using System.Globalization;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportNFDepartureDayController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportNFDepartureDayController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
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
                string Arquivo = Library.CarregarArquivo(_configuration);
                if (Arquivo.Contains("|") == false || Arquivo.Trim().Length == 0)
                {
                    return BadRequest("Arquivo Invalido!");
                }

                List<C100> list = new List<C100>();
                foreach (string linha in Arquivo.Split('\n'))
                {
                    if (linha != "")
                    {
                        string[] dados = linha.Split("|");

                        //C100 
                        if (dados[1] == "C100")
                        {
                            C100 c100 = new C100();
                            list.Add(c100.MontaDadosC100(dados));
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

                    var dataFormat = data
                                        .Select()
                                        .Where(x => Library.GetInt16(x["IND_OPER"].ToString()) == 1 && Library.GetDateTime(x["DT_DOC"].ToString()) >= dateStart && Library.GetDateTime(x["DT_DOC"].ToString()) <= dateEnd) //1-IND OPER => NOTAS DE SAIDA
                                        .GroupBy(g => new
                                        {
                                            grp_data = g["DT_DOC"]
                                        })
                                        .Select(s => new
                                        {
                                            DT_E_S = s.Key.grp_data,
                                            VL_DOC = s.Sum(ss => Library.GetDecimal(ss["VL_DOC"].ToString()))
                                        }).ToList();

                    retRel = JsonConvert.SerializeObject(dataFormat);
                }
            }
            catch
            {
                return BadRequest("Erro ao Executar Relatório.");
            }

            return retRel != null && retRel != ""
                    ? Ok(retRel)
                    : BadRequest("Nenhum Registro Encontrado.");
        }
      
    }
}
