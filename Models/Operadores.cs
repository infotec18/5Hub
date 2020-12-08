using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("operadores")]
    public class Operadores
    {        
        public int CODIGO { get; set; }
        public string NOME { get; set; }
    }
}