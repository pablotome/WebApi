using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.WebApi.Models
{
    public class Usuario
    {
        [Required]
        [StringLength(40)]
        public string Mail { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        public Rol Rol { get; set; }
    }

    public enum Rol
    {
        Usuario = 1,
        Administrador = 2
    }
}
