using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.WebApi.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]

        public string Apellido { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }
        public DateTime FechaAlta { get; set; }
        public IEnumerable<Cuenta> Cuentas { get; set; }
    }
}
