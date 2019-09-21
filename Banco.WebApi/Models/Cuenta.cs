using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.WebApi.Models
{
    public class Cuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Saldo { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }
    }
}
