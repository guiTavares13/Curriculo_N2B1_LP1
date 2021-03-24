using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCurriculos.Models
{
    public class EnderecoViewModel
    {
        public int codEndereco { get; set; }
        public string cep { get; set; }
        public string rua { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }

        public string estado { get; set; }
    }
}
