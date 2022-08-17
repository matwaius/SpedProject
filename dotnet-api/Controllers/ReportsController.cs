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

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportsController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(int parTipoRel)
        {
            bool invalido = false;
            string retRel = "";

            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("UsersAppCon"));
            conn.Open();
            string query = "SELECT DataFiles FROM Files";
            SqlCommand cmd = new SqlCommand(query, conn);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            byte[] buffer = dt.AsEnumerable().Select(c => c.Field<byte[]>("DataFiles")).SingleOrDefault();

            if(buffer == null)
            {
                return BadRequest("Arquivo Não Existe!");
            }

            //Le o Arquivo do Banco
            string Arquivo = Encoding.Default.GetString(buffer);

            //System.IO.File.WriteAllBytes(@"c:\Teste\DataFiles.txt", buffer);

            conn.Close();

            //Exemplo Relatorio
            if(parTipoRel == 1)//Classificar por Tipos?
            {
                //Gera Classe e Atribui

                /*results result = new results();
                result.id = 1;
                result.value = "ABC";
                result.info = "ABC";
                retRel = JsonConvert.SerializeObject(result);*/
            }

            //Validação Caso Necessite Usar
            if (invalido == true)
            {
                return BadRequest("Invalido!");
            }

            return retRel != null && retRel != ""
                ? Ok(retRel)
                : BadRequest("Não foi possivel gerar o Relatório.");
        }
      
    }
}
