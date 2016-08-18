using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoacaoSangue.Web.Models
{
    public class LoginModel
    {
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string EmailUsuario { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Senha { get; set; }
    }
}