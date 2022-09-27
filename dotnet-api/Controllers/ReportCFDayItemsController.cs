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
    public class ReportCFDayItemsController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportCFDayItemsController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromQuery] DateTime date)
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

                List<_0200> list0200 = new List<_0200>();
                List<C460> list = new List<C460>();
                C460 c460 = new C460();
                foreach (string line in file.Split('\n'))
                {
                    if (line != "")
                    {
                        string[] data = line.Split("|");

                        //0200
                        if (data[1] == "0200")
                        {
                            _0200 _0200 = new _0200();
                            list0200.Add(_0200.MountData0200(data));
                        }

                        //C460
                        if (data[1] == "C460")
                        {
                            c460 = new C460();
                            c460.REG = Library.GetString(data[1]);
                            c460.COD_MOD = Library.GetString(data[2]);
                            c460.COD_SIT = Library.GetString(data[3]);
                            c460.NUM_DOC = Library.GetInt32(data[4]);
                            c460.DT_DOC = Library.ToDateTime(data[5], "ddMMyyyy");
                            c460.VL_DOC = Library.GetDecimal(data[6]);
                            c460.VL_PIS = Library.GetDecimal(data[7]);
                            c460.VL_COFINS = Library.GetDecimal(data[8]);
                            c460.CPF_CNPJ = Library.GetString(data[9]);
                            c460.NOM_ADQ = Library.GetString(data[10]);
                            c460.Itens = new List<C470>();
                            list.Add(c460);
                        }

                        //C470
                        if (data[1] == "C470")
                        {
                            C470 c470 = new C470();
                            c470.REG = Library.GetString(data[1]);
                            c470.COD_ITEM = Library.GetString(data[2]);
                            c470.QTD = Library.GetDecimal(data[3]);
                            c470.QTD_CANC = Library.GetDecimal(data[4]);
                            c470.UNID = Library.GetString(data[5]);
                            c470.VL_ITEM = Library.GetDecimal(data[6]);
                            c470.CST_ICMS = Library.GetInt16(data[7]);
                            c470.CFOP = Library.GetInt16(data[8]);
                            c470.ALIQ_ICMS = Library.GetDecimal(data[9]);
                            c470.VL_PIS = Library.GetDecimal(data[10]);
                            c470.VL_COFINS = Library.GetDecimal(data[11]);
                            c470.DESCR_ITEM = list0200.Where(w => w.COD_ITEM == c470.COD_ITEM).Select(s => s.DESCR_ITEM).FirstOrDefault().ToString();
                            c460.Itens.Add(c470);
                        }
                    }
                }

                if (list.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                { 
                    //List<C470> listItens = new List<C470>();
                    //listItens = (List<C470>)list.Select(s => s.Itens.ToList());
                    //foreach (C470 c470 in listItens)
                    //{
                    //    c470.DESCR_ITEM = list0200.Where(w => w.COD_ITEM == c470.COD_ITEM).Select(s => s.DESCR_ITEM).FirstOrDefault().ToString();
                    //}
                    retRel = JsonConvert.SerializeObject(list.Where(w => Library.GetDateTime(w.DT_DOC.ToString()) == date));
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
