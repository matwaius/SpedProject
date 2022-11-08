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

        public short Nivel { get; set; }

        public short RelNF { get; set; }

        public short RelNFICMS { get; set; }

        public short RelNFICMSST { get; set; }

        public short RelNFUF { get; set; }

        public short RelNFQTD { get; set; }

        public short RelNFQTDUF { get; set; }

        public short RelCF { get; set; }

        public short RelCFTOT { get; set; }

    }
}
