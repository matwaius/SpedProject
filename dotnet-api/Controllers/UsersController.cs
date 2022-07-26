using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using dotnet_api.Models;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GET - USERS
        [HttpGet]
        public JsonResult Get(string login, string senha)
        {
            string query = @"
                            select PK_Cod_Usuario, Login, Email from
                            dbo.Usuarios_Cad
                            where Login=@Login and Senha=@Senha
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Login", login);
                    myCommand.Parameters.AddWithValue("@Senha", senha);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        #endregion

        #region POST - USERS 
        [HttpPost]
        public JsonResult Post(Users user)
        {
            string query = @"
                           insert into dbo.Usuarios_Cad
                           values (@Login, @Senha, @Email)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Login", user.Login);
                    myCommand.Parameters.AddWithValue("@Senha", user.Senha);
                    myCommand.Parameters.AddWithValue("@Email", user.Email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        #endregion

        #region PUT - USERS
        [HttpPut]
        public JsonResult Put(Users user)
        {
            string query = @"
                           update dbo.Usuarios_Cad
                           set Login= @Login, Senha = @Senha, Email = @Email
                            where PK_Cod_Usuario=@PK_Cod_Usuario
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PK_Cod_Usuario", user.PK_Cod_Usuario);
                    myCommand.Parameters.AddWithValue("@Login", user.Login);
                    myCommand.Parameters.AddWithValue("@Senha", user.Senha);
                    myCommand.Parameters.AddWithValue("@Email", user.Email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        #endregion

        #region DELETE - USERS
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Usuarios_Cad
                            where PK_Cod_Usuario=@PK_Cod_Usuario
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PK_Cod_Usuario", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
        #endregion

    }
}