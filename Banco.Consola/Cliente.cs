using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Banco.Consola
{
    [DataContract(Name = "cliente")]
    public class Cliente
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "apellido")]
        public string Apellido { get; set; }
        [DataMember(Name = "nombre")]
        public string Nombre { get; set; }
        [DataMember(Name = "fechanacimiento")]
        public DateTime? FechaNacimiento { get; set; }
        [DataMember(Name = "fechaalta")]
        public DateTime FechaAlta { get; set; }
    }
}
