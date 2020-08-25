using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Models
{
    public partial class Produtos
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        [JsonIgnore]
        public bool Delete { get; set; } = false;
    }
}
