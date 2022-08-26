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
    public class ReportNFDepartureDayByUFController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportNFDepartureDayByUFController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
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

                List<_0150> list0150 = new List<_0150>();
                List<C100> listC100 = new List<C100>();
                foreach (string line in file.Split('\n'))
                {
                    if (line != "")
                    {
                        string[] data = line.Split("|");

                        //0150 
                        if (data[1] == "0150")
                        {
                            _0150 _0150 = new _0150();
                            list0150.Add(_0150.MountData0150(data));
                        }

                        //C100 
                        if (data[1] == "C100")
                        {
                            C100 c100 = new C100();
                            listC100.Add(c100.MountDataC100(data));
                        }
                    }
                }

                if (list0150.Count == 0 || listC100.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                {
                    DataTable data0150 = Library.ToDataTable(list0150);
                    DataTable dataC100 = Library.ToDataTable(listC100);
                    DataTable dataUF = Library.dataUF();

                    //LINQ
                    var results = from t0150 in data0150.AsEnumerable()
                                  join tC100 in dataC100.AsEnumerable() on Library.GetString(t0150["COD_PART"].ToString()) equals Library.GetString(tC100["COD_PART"].ToString())
                                  join tUF in dataUF.AsEnumerable() on Library.GetInt32(t0150["COD_MUN"].ToString().Substring(0, 2)) equals Library.GetInt32(tUF["id"].ToString())
                                  where Library.GetInt16(tUF["IND_OPER"].ToString()) == 1 && Library.GetDateTime(tUF["DT_DOC"].ToString()) >= dateStart && Library.GetDateTime(tUF["DT_DOC"].ToString()) <= dateEnd
                                  select new
                                  {
                                      UF = tUF["UF"].ToString(),
                                      VL_DOC = Library.GetDecimal(tC100["VL_DOC"].ToString())
                                  }
                                  into xResult
                                  group xResult by new
                                  {
                                      UF = xResult.UF
                                  } into xGroup
                                  select new
                                  {
                                      UF = xGroup.Key.UF,
                                      VL_DOC = xGroup.Sum(temp => temp.VL_DOC)
                                  };

                    retRel = JsonConvert.SerializeObject(results);
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
