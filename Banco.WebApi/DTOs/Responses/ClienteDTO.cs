using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.WebApi.DTOs.Responses
{
    public class ClienteDTO
    {
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
}
