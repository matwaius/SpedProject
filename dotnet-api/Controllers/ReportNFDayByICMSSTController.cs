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
    public class ReportNFDayByICMSSTController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportNFDayByICMSSTController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd, [FromQuery] Int16 indOperacao)
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

                List<C100> listC100 = new List<C100>();
                foreach (string line in file.Split('\n'))
                {
                    if (line != "")
                    {
                        string[] data = line.Split("|");

                        //C100 
                        if (data[1] == "C100")
                        {
                            C100 c100 = new C100();
                            listC100.Add(c100.MountDataC100(data));
                        }
                    }
                }

                if (listC100.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                {
                    DataTable data = Library.ToDataTable(listC100);

                    var dateFormat = data
                                        .Select()
                                        .Where(x => Library.GetInt16(x["IND_OPER"].ToString()) == indOperacao && Library.GetDateTime(x["DT_DOC"].ToString()) >= dateStart && Library.GetDateTime(x["DT_DOC"].ToString()) <= dateEnd && Library.GetDecimal(x["VL_ICMS_ST"].ToString()) > 0) 
                                        .GroupBy(g => new
                                        {
                                            grp_date = g["DT_DOC"].ToString().Substring(0,10)
                                        })
                                        .Select(s => new
                                        {
                                            DT_DOC = s.Key.grp_date,
                                            VL_ICMS_ST = s.Sum(ss => Library.GetDecimal(ss["VL_ICMS_ST"].ToString()))
                                        }).ToList();

                    retRel = JsonConvert.SerializeObject(dateFormat) ;
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
