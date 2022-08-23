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
    public class ReportNFSaidaController : ControllerBase
    {
        private readonly IFilesRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        public ReportNFSaidaController(IFilesRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            bool invalido = false;
            string retRel = "";

            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("UsersAppCon"));
            conn.Open();
            string query = "SELECT top 1 DataFiles FROM Files ORDER BY id Desc";
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
            if (Arquivo.Contains("|") == false)
            {
                return BadRequest("Arquivo Invalido!");
            }

            DataSet dsRet = new DataSet();
            List<C100> list = new List<C100>();
            foreach (string linha in Arquivo.Split('\n'))
            {
                if (linha != "")
                {
                    string[] dados = linha.Split("|");
                    try
                    {
                        //C100 //1-IND OPER => NOTAS DE SAIDA
                        if (dados[1] == "C100" && dados[2] == "1")
                        {
                            C100 c100 = new C100();
                            list.Add(c100.MontaDadosC100(dados));
                        }
                    }
                    catch (Exception ex)
                    {
                        int ERRO = 0;
                        throw;
                    }
                }
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable data = converter.ToDataTable(list);

            conn.Close();

            //Exemplo Relatorio

                //Gera Classe e Atribui

                /*results result = new results();
                result.id = 1;
                result.value = "ABC";
                result.info = "ABC";
                retRel = JsonConvert.SerializeObject(result);*/

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
