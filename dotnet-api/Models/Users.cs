using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api.Models
{
    public class Users
    {
        public int PK_Cod_Usuario { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

    }
}
