using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("clientesomnimsg")]
    public class ClientesOmniMsg
    {        
        public int? id { get; set; }
        public int? talk_id { get; set; }
        public int? user_id { get; set; }
        public int? channel_id { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public DateTime sent_at { get; set; }
    }
   
    
}