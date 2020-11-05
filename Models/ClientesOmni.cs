using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("clientesomni")]
     public class ClientesOmni
    {
        public int talk_id { get; set; }
        public int? customer_id { get; set;}
        public int? channel_id { get; set;}
        public DateTime finished_at { get; set; }
        public DateTime created_at { get; set; }
        public string json { get; set; }
    }
}