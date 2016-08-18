using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoacaoSangue.AuthServerApi.Entidade
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Roles { get; set; }
        public string ClientId { get; set; }
    }
}