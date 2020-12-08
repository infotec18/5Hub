using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("clientes")]
    public class Clientes
    {        
        public int CODIGO { get; set; }
        public string RAZAO { get; set; }
        public string EMAIL { get; set; }
        public string CPF_CNPJ { get; set; }
        public int? AREA1 { get; set; }
        public string FONE1 { get; set; }
        public int? AREA2 { get; set; }
        public string FONE2 { get; set; }
        public int? AREA3 { get; set; }
        public string FONE3 { get; set; }
        [NotMapped]
        public int? OPERADOR { get; set; }
        [NotMapped]
        public string OPERADOR_NOME { get; set; }
    }
}