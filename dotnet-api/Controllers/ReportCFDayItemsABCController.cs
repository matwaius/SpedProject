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
    public class ReportCFDayItemsABCController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportCFDayItemsABCController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd, [FromQuery] decimal A, [FromQuery] decimal B, [FromQuery] decimal C, [FromQuery] string CurvaSel)
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

                List<_0200> list0200 = new List<_0200>();
                List<C470> list = new List<C470>();
                decimal total = 0;
                bool add = false;
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
                            add = false;
                            if (Library.ToDateTime(data[5], "ddMMyyyy") >= dateStart  && Library.ToDateTime(data[5], "ddMMyyyy") <= dateEnd)
                            {
                                add = true;
                            }
                        }

                        //C470
                        if (data[1] == "C470" && add == true)
                        {
                            C470 c470 = new C470();
                            c470.REG = Library.GetString(data[1]);
                            c470.COD_ITEM = Library.GetString(data[2]);
                            c470.QTD = Library.GetDecimal(data[3]);
                            c470.QTD_CANC = Library.GetDecimal(data[4]);
                            c470.UNID = Library.GetString(data[5]);
                            c470.VL_ITEM = Library.GetDecimal(data[6]);
                            total += c470.VL_ITEM;
                            c470.CST_ICMS = Library.GetInt16(data[7]);
                            c470.CFOP = Library.GetInt16(data[8]);
                            c470.ALIQ_ICMS = Library.GetDecimal(data[9]);
                            c470.VL_PIS = Library.GetDecimal(data[10]);
                            c470.VL_COFINS = Library.GetDecimal(data[11]);
                            c470.DESCR_ITEM = "";
                            c470.PERC = 0;
                            list.Add(c470);
                        }
                    }
                }

                if (list.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                {
                    decimal Acumulado = 0;
                    foreach (C470 c470 in list.OrderByDescending(o => o.VL_ITEM))
                    {
                        c470.DESCR_ITEM = list0200.Where(w => w.COD_ITEM == c470.COD_ITEM).Select(s => s.DESCR_ITEM).FirstOrDefault().ToString();
                        c470.PERC = (c470.VL_ITEM * 100) / total;
                        Acumulado += c470.PERC;
                        if (Acumulado <= A && A > 0) //Curva A
                        {
                            c470.CURVA = "A";
                        }
                        else if (Acumulado >= A && Acumulado <= (A + B) && B > 0) //Curva B
                        {
                            c470.CURVA = "B";
                        }
                        else if (Acumulado >= (A + B) && Acumulado <= (A + B + C) && C > 0) //Curva C
                        {
                            c470.CURVA = "C";
                        }
                    }
                    retRel = JsonConvert.SerializeObject(list.Where(w => w.CURVA == CurvaSel));
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
