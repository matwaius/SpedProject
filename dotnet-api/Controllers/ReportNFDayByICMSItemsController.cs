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
    public class ReportNFDayByICMSItemsController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportNFDayByICMSItemsController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromQuery] DateTime date, [FromQuery] Int16 indOperacao)
        {
            string retRel = "";
            try
            {
                string teste = indOperacao.ToString();
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

                List<C170> list = new List<C170>();
                bool add = false;
                foreach (string line in file.Split('\n'))
                {
                    if (line != "")
                    {
                        string[] data = line.Split("|");

                        //C100
                        if (data[1] == "C100")
                        {
                            add = false;
                            if (Library.GetInt16(data[2]) == indOperacao)
                            {
                                if (date == Library.ToDateTime(data[10], "ddMMyyyy"))
                                {
                                    add = true;
                                }
                            }
                        }

                        //C170
                        if (data[1] == "C170" && add == true)
                        {
                            C170 c170 = new C170();
                            c170.REG = Library.GetString(data[1]);
                            c170.NUM_ITEM = Library.GetInt32(data[2]);
                            c170.COD_ITEM = Library.GetString(data[3]);
                            c170.DESCR_COMPL = Library.GetString(data[4]);
                            c170.QTD = Library.GetDecimal(data[5]);
                            c170.UNID = Library.GetString(data[6]);
                            c170.VL_ITEM = Library.GetDecimal(data[7]);
                            c170.VL_DESC = Library.GetDecimal(data[8]);
                            c170.IND_MOV = Library.GetInt16(data[9]);
                            c170.CST_ICMS = Library.GetString(data[10]);
                            c170.CFOP = Library.GetInt16(data[11]);
                            c170.COD_NAT = Library.GetString(data[12]);
                            c170.VL_BC_ICMS = Library.GetDecimal(data[13]);
                            c170.ALIQ_ICMS = Library.GetDecimal(data[14]);
                            c170.VL_ICMS = Library.GetDecimal(data[15]);
                            c170.VL_BC_ICMS_ST = Library.GetDecimal(data[16]);
                            c170.ALIQ_ST = Library.GetDecimal(data[17]);
                            c170.VL_ICMS_ST = Library.GetDecimal(data[18]);
                            c170.IND_APUR = Library.GetInt16(data[19]);
                            c170.CST_IPI = Library.GetString(data[20]);
                            c170.COD_ENQ = Library.GetString(data[21]);
                            c170.VL_BC_IPI = Library.GetDecimal(data[22]);
                            c170.ALIQ_IPI = Library.GetDecimal(data[23]);
                            c170.VL_IPI = Library.GetDecimal(data[24]);
                            c170.CST_PIS = Library.GetString(data[25]);
                            c170.VL_BC_PIS = Library.GetDecimal(data[26]);
                            c170.ALIQ_PIS_PERC = Library.GetDecimal(data[27]);
                            c170.QUANT_BC_PIS = Library.GetDecimal(data[28]);
                            c170.ALIQ_PIS_REAIS = Library.GetDecimal(data[29]);
                            c170.VL_PIS = Library.GetDecimal(data[30]);
                            c170.CST_COFINS = Library.GetString(data[31]);
                            c170.VL_BC_COFINS = Library.GetDecimal(data[32]);
                            c170.ALIQ_COFINS_PERC = Library.GetDecimal(data[33]);
                            c170.QUANT_BC_COFINS = Library.GetDecimal(data[34]);
                            c170.ALIQ_COFINS_REAIS = Library.GetDecimal(data[35]);
                            c170.VL_COFINS = Library.GetDecimal(data[36]);
                            c170.COD_CTA = Library.GetString(data[37]);
                            c170.VL_ABAT_NT = Library.GetDecimal(data[38]);
                            c170.PERC = 0;
                            c170.CURVA = "";
                            list.Add(c170);
                        }
                    }
                }

                if (list.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                {
                    retRel = JsonConvert.SerializeObject(list.Where(w => w.VL_ICMS > 0));
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
