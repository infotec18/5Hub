using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("campanhas_clientes")]
    public class CampanhasClientes
    {        
        public int CODIGO { get; set; }
        public int? CLIENTE { get; set; }
        public int? OPERADOR { get; set; }
        public string CONCLUIDO { get; set; }
    }
}