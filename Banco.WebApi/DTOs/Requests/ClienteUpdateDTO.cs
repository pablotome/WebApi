using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.WebApi.DTOs.Requests
{
    public class ClienteUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]

        public string Apellido { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }
    }
}
