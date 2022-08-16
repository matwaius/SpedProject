using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_api.Models.Entities
{
    public class Users : Base
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

    }
}
