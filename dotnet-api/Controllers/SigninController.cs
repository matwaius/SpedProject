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
    public class SigninController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SigninController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region POST - Signin 
        [HttpPost]
        public JsonResult Post(Users user)
        {
            string query = @"
                           select top 1 user_login, email from dbo.Users
                           where user_login = @user_login and password = @password";

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
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

                if (table.Rows.Count > 0)
                {
                    return new JsonResult("User Logged.");
                }
            }
            return new JsonResult("User Not Found.");
        }
        #endregion

    }
}
