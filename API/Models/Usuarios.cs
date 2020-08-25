using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Models
{
    public partial class Usuarios
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public bool Delete { get; set; } = false;
    }
}
