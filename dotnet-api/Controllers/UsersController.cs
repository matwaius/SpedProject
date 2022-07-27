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
        public JsonResult Get()
        {
            string query = @"
                            select id_user, user_login, email from
                            dbo.Users
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
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
                           insert into dbo.Users
                           values (@user_login, @password, @email)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@user_login", user.user_login);
                    myCommand.Parameters.AddWithValue("@password", user.password);
                    myCommand.Parameters.AddWithValue("@email", user.email);
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
                           update dbo.Users
                           set user_login= @user_login, password = @password, email = @email
                            where id_user=@id_user
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_user", user.id_user);
                    myCommand.Parameters.AddWithValue("@user_login", user.user_login);
                    myCommand.Parameters.AddWithValue("@password", user.password);
                    myCommand.Parameters.AddWithValue("@email", user.email);
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
                           delete from dbo.Users
                            where id_user=@id_user
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UsersAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_user", id);

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