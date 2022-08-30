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

                List<C405B> list = new List<C405B>();

                C405B c405B = new C405B();
                int cont = 0;
                foreach (string line in file.Split('\n'))
                {
                    if (line != "")
                    {
                        string[] data = line.Split("|");

                        //C405 
                        if (data[1] == "C405")
                        {
                            cont = 0;
                            if (cont == 0 && c405B.VL_BRT > 0)
                            {
                                list.Add(c405B);
                            }
                            c405B = new C405B();
                            c405B.DT_DOC = Library.ToDateTime(data[2], "ddMMyyyy");
                            c405B.VL_BRT = Library.GetDecimal(data[7]);
                            c405B.Itens = new List<C420B>();
                            cont++;
                        }

                        //C420 
                        if (data[1] == "C420")
                        {
                            C420B c420B = new C420B();
                            c420B.COD_TOT_PAR = Library.GetString(data[2]);
                            c420B.VLR_ACUM_TOT = Library.GetDecimal(data[3]);
                            c405B.Itens.Add(c420B);
                        }
                    }
                }

                if (list.Count == 0)
                {
                    return BadRequest("Nenhum Registro Encontrado.");
                }
                else
                {
                    retRel = JsonConvert.SerializeObject(list.Where(w => Library.GetDateTime(w.DT_DOC.ToString()) >= dateStart && Library.GetDateTime(w.DT_DOC.ToString()) <= dateEnd));
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
